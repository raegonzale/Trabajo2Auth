// See https://aka.ms/new-console-template for more information
using Application.Controllers;
using Application.Ports;
using Application.Services;
using Domain.Auth.Chain;
using Domain.Auth.Interfaces;
using Domain.Auth.Strategies;
using Domain.Org.Entities;
using Infrastructure.Adapters;
using Infrastructure.Logging;
using Infrastructure.Persistence;
using Legacy;


// Bootstrap – repos y auditoría
IUserRepository repo = new InMemoryUserRepository();
IAuditSink audit = new InMemoryAudit();


// Árbol organizacional de ejemplo
var sedeBogota = new Sede("Bogotá");
var depTI = new Dependencia("TI");
var depOps = new Dependencia("Operaciones");
sedeBogota.AddChild(depTI);
sedeBogota.AddChild(depOps);


// Usuarios legacy y seed
var u1 = new User(); u1.setFirstname("Ana"); u1.setLastname("López"); u1.setUsername("alopez"); u1.setPassword("1234");
var u2 = new User(); u2.setFirstname("Carlos"); u2.setLastname("Pérez"); u2.setUsername("cperez"); u2.setPassword("abcd");
var u3 = new User(); u3.setFirstname("Diego"); u3.setLastname("Rojas"); u3.setUsername("drojas"); u3.setPassword("pass");
repo.Save(u1); repo.Save(u2); repo.Save(u3);


depTI.AddChild(new Usuario(u1));
depTI.AddChild(new Usuario(u2));
depOps.AddChild(new Usuario(u3));


// Strategy basada en verificador local (infra)
var verifier = new LocalCredentialsVerifier(repo);
var strategy = new LocalStrategy(verifier);


// Pipeline: Audit -> Format -> Lockout -> Strategy
var builder = new AuthPipelineBuilder()
.WithAudit(audit)
.WithFormatValidator()
.WithLockoutGuard(max: 3, lockFor: TimeSpan.FromMinutes(1))
.WithStrategy(strategy);


var authService = new AuthService(builder);
var controller = new LoginController(authService);
var orgService = new OrgService();


Heading("CHECKPOINT 1 – Login válido/ inválido y bloqueo");
Check("Login válido (alopez/1234)", controller.login("alopez", "1234") == true);
Check("Login inválido (alopez/wrong)", controller.login("alopez", "wrong") == false);
Check("Login inválido (intento 2)", controller.login("alopez", "wrong") == false);
Check("Login inválido (intento 3 → bloquea)", controller.login("alopez", "wrong") == false);
Check("Aún bloqueado (con credenciales correctas)", controller.login("alopez", "1234") == false);


Heading("CHECKPOINT 2 – Listado de empleados (BFS)");
var lista = orgService.listarEmpleados(sedeBogota);
foreach (var line in lista) Console.WriteLine(" - " + line);
Check("Listado contiene 3 usuarios", lista.Count == 3);


Heading("CHECKPOINT 3 – Búsqueda (DFS + Criteria)");
var encontrados = orgService.buscarUsuarios(sedeBogota, new Domain.Search.BusquedaCriteria { Firstname = "a" });
foreach (var u in encontrados) Console.WriteLine($" * {u.GetUsername()} -> {u.GetName()}");
Check("Búsqueda retorna 3", encontrados.Count == 2);


Heading("CHECKPOINT 4 – Auditoría del pipeline");
foreach (var e in (audit as InMemoryAudit)!.Entries) Console.WriteLine(e);


Console.WriteLine("\nListo. Arquitectura por capas funcionando.\n");


static void Heading(string title)
{
    Console.WriteLine("\n================ " + title + " ================");
}


static void Check(string name, bool condition)
{
    var (tag, color) = condition ? ("[OK]", ConsoleColor.Green) : ("[FAIL]", ConsoleColor.Red);
    var prev = Console.ForegroundColor;
    Console.ForegroundColor = color; Console.Write(tag); Console.ForegroundColor = prev;
    Console.WriteLine(" " + name);
}