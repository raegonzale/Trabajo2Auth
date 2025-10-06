**Trabajo2Auth**



Sistema de autenticación compatible con legado usando Strategy + Chain of Responsibility + Composite + Iterator sobre una arquitectura por capas (.NET 8).



**Objetivo:** reemplazar el método checkUsernameAndPassword de Legacy.User sin romper consumidores, y sumar políticas (validación, bloqueo, auditoría) y un modelo organizacional navegable.



**Arquitectura:**



**Capas y responsabilidades:**



**Legacy:** clase heredada User (intocable).

**Domain:** reglas de negocio y patrones (Strategy, CoR, Composite, Iterator, Criteria). Sin dependencias de infraestructura.

**Application:** orquestación de casos de uso (AuthService, OrgService) y LoginController. Expone la misma firma que Legacy.User para compatibilidad.

**Infrastructure**: adaptadores concretos (repo en memoria, auditoría en memoria, verificadores de credenciales Local/LDAP/OAuth).

**Presentation.Console:** app de consola para bootstrap y checkpoints.



**Patrones aplicados:**



**Strategy (IAuthStrategy):** Local/Ldap/OAuth sin cambiar clientes.

**Chain of Responsibility (IHandler):** AuditTrail → FormatValidator → LockoutGuard → StrategyInvoker.

**Composite (IOrgNode):** Sede, Dependencia, Usuario (hoja que envuelve Legacy.User).

**Iterator (IIterator<T>):** recorridos DFS/BFS del árbol organizacional.

**Criteria (BusquedaCriteria):** encapsula filtros de búsqueda.



**Estructura del repositorio**

Trabajo2Auth.sln

├─ Legacy/

│  └─ User.cs

├─ Domain/

│  ├─ Auth/

│  │  ├─ Interfaces/ (IAuthStrategy, IHandler, IAuditSink, ICredentialsVerifier)

│  │  ├─ DTOs/ (AuthRequest, AuthResult)

│  │  ├─ Chain/ (HandlerBase, FormatValidator, LockoutGuard, AuditTrail, StrategyInvoker, AuthPipelineBuilder)

│  │  └─ Strategies/ (LocalStrategy, LdapStrategy, OAuthStrategy)

│  ├─ Org/

│  │  ├─ Interfaces/ (IOrgNode)

│  │  └─ Entities/ (Sede, Dependencia, Usuario)

│  ├─ Iteration/

│  │  ├─ Interfaces/ (IIterator)

│  │  └─ Implementations/ (DFSIterator, BFSIterator, Iterators)

│  └─ Search/ (BusquedaCriteria)

├─ Application/

│  ├─ Ports/ (IUserRepository)

│  ├─ Services/ (AuthService, OrgService)

│  └─ Controllers/ (LoginController)

├─ Infrastructure/

│  ├─ Persistence/ (InMemoryUserRepository)

│  ├─ Logging/ (InMemoryAudit)

│  └─ Adapters/ (LocalCredentialsVerifier, LdapCredentialsVerifier, OAuthCredentialsVerifier)

└─ Presentation.Console/

&nbsp;  └─ Program.cs   (bootstrap + checkpoints)



**Requisitos**



.NET SDK 8.0

Verifica con: dotnet --version



Ejecución rápida

dotnet build

dotnet run --project Presentation.Console





**Salida esperada (resumen):**

**Checkpoint 1:** Login válido/ inválido + bloqueo tras N fallos.

**Checkpoint 2:** Listado BFS de empleados (3 usuarios de ejemplo).

**Checkpoint 3:** Búsqueda DFS por criterio.

**Checkpoint 4:** Auditoría de intentos/resultados.



**Nota:** Si usas Firstname = "a", por defecto coinciden 2 usuarios (“Ana”, “Carlos”).

Cambia el aserto a 2 o renombra el tercero para que contenga “a”.



**Configuración y extensibilidad**

Pipeline de autenticación



Se arma en Program.cs con el builder:



var builder = new AuthPipelineBuilder()

&nbsp; .WithAudit(audit)               // IAuditSink

&nbsp; .WithFormatValidator()          // regex usuario + password not empty

&nbsp; .WithLockoutGuard(max: 3, lockFor: TimeSpan.FromMinutes(1))

&nbsp; .WithStrategy(strategy);        // IAuthStrategy





Agregar pasos: crea un HandlerBase nuevo (ej. MFA, geofencing) y añádelo al builder.

Cambiar orden: reordena las llamadas del builder.

Estrategias de autenticación

LocalStrategy usa ICredentialsVerifier (en Infrastructure, LocalCredentialsVerifier valida contra IUserRepository).

LdapStrategy / OAuthStrategy: stubs listos para implementar.



Para cambiar de estrategia:

IAuthStrategy strategy = new LocalStrategy(verifier);

// …o: new LdapStrategy(), new OAuthStrategy()



Bloqueo por intentos

LockoutGuard(max, lockFor) recibe:

max: intentos fallidos antes de bloquear

lockFor: duración del bloqueo



Auditoría

IAuditSink permite redirigir la auditoría:

Demo: InMemoryAudit

Producción: cambia por Serilog, archivo, base de datos, etc.

Organización y búsqueda

Recorridos: Iterators.Bfs(root) / Iterators.Dfs(root).

Búsquedas: BusquedaCriteria.Matches(Usuario) (amplía con Sede, Dependencia, etc.).



