**[English Instructions for installing your environment](#english-instructions)**

**[Instrucciones en castellano para instalar tu entorno](#instalación-de-vs-code-y-herramientas-relacionadas)**

# Instalación de VS Code y herramientas relacionadas

## Instalación VS Code
Descargar e instalar [VS Code](https://code.visualstudio.com/download?_exp_download=fb315fc982)

## Control de Versiones: Instalar Git 
Descargar e instalar Git for Windows (Mac o Linux dependiendo de tu máquina) usando las opciones por defecto [GIT](https://git-scm.com/install/windows)

## Instalar para desarrollo

Descargar e instalar en tu máquina [.NET10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0).

Descargar e instalar en tu máquina SQL Server 2025 Express Edition [SQL Server 2025 Express Edition](https://learn.microsoft.com/es-es/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver17#install-localdb)

## Instalar extensiones en VS Code

Si ya estás **usando VS Code**, te recomendamos crear un profile para instalar las extensiones siguiendo las instrucciones proporcionados en Campus Virtual [VS Code profile](https://campusvirtual.uclm.es/mod/resource/view.php?id=1364110)


Puedes **instalar todas las extensiones** de la siguiente forma:
1. abre una terminal
2. cambia a la raiz de la solución donde está el archivo **extensions4VSCode.txt**
3. ejecuta el siguiente comando:

```bash
Get-Content extensions4VSCode.txt | ForEach-Object { code --install-extension $_ }
```

**Alternativamente*, puedes abrir cada extensión una a una. Para ello, abre la vista de extensiones (ctrl+shift+x).


![Extensiones](https://code.visualstudio.com/assets/docs/configure/extensions/extension-marketplace/extensions-view-icon.png)


![Extensiones](https://code.visualstudio.com/assets/docs/configure/extensions/extension-marketplace/extensions-view-icon.png)

Instalar en VS Code las extensiones para desarrollo:
- C#
- C# Namespace autocompletion
- C# Dev Kit
- .NET Install Tool
- .Net Maui
- Microsoft.AspNetCore.Razor.VSCode.BlazorWasmDebuggingExtension
- MSSQL
- Open in Browser
- PlantUML
- GitHub Actions

Instalar para testing las siguientes extensiones:
- .Net Core Test Explorer
- Coverage Gutters 

Instalar las siguientes extensiones en VS Code para Git:
- GitHub Pull Requests: para control de versiones
- Git Graph: Git Graph del repositorio
- Git History: ver el log e historia de los archivos.


## Para aquellos equipos que vayan a desarrollar su proyecto con MAUI para la asignatura de IPO:
Seguir las instrucciones que se indican en el siguiente enlace [MAUI](https://learn.microsoft.com/es-es/dotnet/maui/get-started/installation?view=net-maui-10.0&tabs=visual-studio-code#connect-your-account-to-c-dev-kit)
Ya se proporciona un proyecto para desarrollo AppForSEII.MAUI por lo que no es necesario su creación.



# Preparar el proyecto para iniciar el desarrollo

## Crea el repositorio:
Clona la plantilla del proyecto

## Instala las herramientas para Entity Framework en el proyecto ejecutando en el terminal el siguiente comando (View\Terminal):

```bash
dotnet tool install --global dotnet-ef
```

## Instala las herramientas de desarrollo, abriendo un terminal en VS code (View\Terminal):
```bash
dotnet tool install --global NSwag.ConsoleCore

```
## Instala ReportGenerator como herramienta .NET:

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```


## Instala las herramientas para generación de modelos UML a partir de código en tu proyecto:

```bash
dotnet tool install --global PlantUmlClassDiagramGenerator
```


# Desarrolla tu proyecto 

## Compilar, limpiar y depurar

1. Abre el terminal integrado en VS Code Menú: View → Terminal (o Ctrl + ñ en teclado español). 
2. Situate en la carpeta del proyecto con el que quieres trabajar (.csproj).
3. Compilar. Si estas en la carpeta de un proyecto, compilará ese proyecto, si estás en la carpeta de la solución, compilará todos los proyectos:

```bash
dotnet build
```

Para compilar en modo release:

```bash
dotnet build -c Release
```

Esto:
        Compila el código
        Restaura paquetes NuGet si es necesario
        Genera la salida en bin/Debug/net10.0/
4.  Restaurar paquetes antes de compilar (por si hay dependencias nuevas):
    dotnet restore
5.  Ejecutar la aplicación (si estas en la carpeta de la Web API):

```bash
dotnet run
```

Puedes especificar un proyecto si estás en la raíz de la solución:

```bash
dotnet run --project MyApi/MyApi.csproj
```

6. En caso de necesitar limpiar el proyecto o la solución:

```bash
dotnet clean
```

## Ejecutar tareas

CTRL+Shift+p: Ejecutar tareas: Depurar, Build, etc.

## Refactorización

Ctrl+shift+r: refactorizaciones soportadas por Roslynator.
Ctrl+.: Genera código: Selecciona atributos para generar constructores y método equals.

## Trabajar con migraciones:

En el terminal integrado de VS Code, sitúate en la carpeta APPForSEII.API que contiene el .csproj donde están las clases de modelo:

```bash
cd ruta/de/tu/proyecto/APPForSEII.API 
```

- Crear la migración en la carpeta Migrations/ con los archivos necesarios:

```bash
dotnet ef migrations add CreateIdentitySchema
```

- Ver el estado actual del modelo:

```bash
dotnet ef migrations list
```

- Eliminar la última migración:

```bash
dotnet ef migrations remove
```

- Aplicar la migración a la BD

```bash
dotnet ef database update
```
- Eliminar todas las migraciones de la BD

```bash
dotnet ef database update 0
```

- Borrar la BD

```bash
dotnet ef database drop
```

## Generar diagramas desde código

Al ejecutar el siguiente comando desde la carpeta de la solución:

```bash
puml-gen ./src/AppForSEII.API/Models  ./src/AppForSEII.API/ClassDiagram -dir -excludePaths **/bin,**/obj,**/Migrations  -createAssociation  -allInOne
```

cuyas opciones son:

- dir procesa directorios de entrada/salida.
- excludePaths evita ruido de bin/ y obj/.
- createAssociation detecta asociaciones desde campos/props.
- allInOne crea un include.puml para agrupar todo. [github.com], [deepwiki.com]

Se generan .puml (y un include.puml si usas -allInOne) que puedes abrir y previsualizar en VS Code con la extensión PlantUML. La extensión recomienda el render por servidor (evita instalar Java/Graphviz), y soporta exportación a PNG/SVG. [marketplac...studio.com]

Para ver el diagrama, abrir include.puml y pulsar Alt+d

## Generación de API Client

Para generar el Cliente de la API en el proyecto web, realiza los siguientes pasos.
1. Abre un terminal y ejecuta la api
2. Copia la ruta al fichero swagger.json
3. Abre otro terminal y cambia al directorio donde este el proyecto web. Ejecuta el comando nswag reemplazando la ruta http que aparece a continuación por la ruta de tu fichero swagger:

```bash
cd src
cd AppForSEII.Web

nswag openapi2csclient /input:http://localhost:5180/swagger/v1/swagger.json /classname:AppForSEIIAPIClient /namespace:AppForSEII.Web.API /output:AppForSEIIAPIClient.cs

```


## Testing: Usar coverlet collector

1. Añade el paquete coverlet.collector a tu proyecto de pruebas y ejecuta:

```bash
dotnet add <TU_PROYECTO_TEST>.csproj package coverlet.collector
```

2. Ejecuta la **generación de cobertura**.

Genera Cobertura bajo TestResults/<GUID>/coverage.cobertura.xml. Coverlet está integrado con VSTest, y esta es la forma recomendada para recoger cobertura en proyectos .NET (funciona con MSTest, xUnit, NUnit):

```bash
dotnet test --collect:"XPlat Code Coverage"
```

3. Genera el informe

```bash
reportgenerator   -reports:"TestResults/**/coverage.cobertura.xml"   -targetdir:"coverage-report"   -reporttypes:Html
```

4. Visualiza la cobertura de las pruebas

Con **Coverage Gutters**: CTRL+SHIT+P: Coverage Gutter: Display.

# ENGLISH INSTRUCTIONS

# Installing VS Code and Related Tools

## Install VS Code

Download and install **VS Code**:

https://code.visualstudio.com/download

## Version Control: Install Git

Download and install **Git for Windows** (or the Mac/Linux version depending on your machine) using the default installation options:

https://git-scm.com/install/windows

## Install Development Tools

Download and install **.NET 10** on your machine:

https://dotnet.microsoft.com/en-us/download/dotnet/10.0

Download and install **SQL Server 2025 Express Edition (LocalDB)**:

https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver17#install-localdb

## Install VS Code Extensions

If you are **already using VS Code**, we encourage to create a profile for installing the extensions following the instructions provided in Campus Virtual [VS Code profile](https://campusvirtual.uclm.es/mod/resource/view.php?id=1364103)

You can **install all extensions automatically**:

1. Open a terminal.
2. Change to the solution root directory containing the **extensions4VSCode.txt** file (use cd command).
3. Run the following command:

```powershell
Get-Content extensions4VSCode.txt | ForEach-Object { code --install-extension $_ }
```

**Alternatively**, open the View Extensions (View\Extensions or **Ctrl+Shift+X**).


![Extensions](https://code.visualstudio.com/assets/docs/configure/extensions/extension-marketplace/extensions-view-icon.png)

Install the following extensions for development:

- C#
- C# Namespace Autocompletion
- C# Dev Kit
- .NET Install Tool
- .NET MAUI
- Microsoft.AspNetCore.Razor.VSCode.BlazorWasmDebuggingExtension
- MSSQL
- Open in Browser
- PlantUML
- GitHub Actions

Install the following extensions for testing:

- .NET Core Test Explorer
- Coverage Gutters

Install the following Git-related extensions:

- GitHub Pull Requests: version control integration
- Git Graph: repository Git graph visualization
- Git History: view file history and commit logs



## For Teams Developing a MAUI Project for the HCI Course

Follow the instructions provided at:

https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation?view=net-maui-10.0&tabs=visual-studio-code#connect-your-account-to-c-dev-kit

A project named **AppForSEII.MAUI** is already provided, so there is no need to create it from scratch.

# Prepare the Project Before Starting Development

## Create the Repository

Clone the project template repository.

## Install Entity Framework Tools

Run the following command from a terminal (**View → Terminal**):

```bash
dotnet tool install --global dotnet-ef
```

## Install Development Tools

Open a terminal in VS Code (**View → Terminal**) and run:

```bash
dotnet tool install --global NSwag.ConsoleCore
```

## Install ReportGenerator

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```

## Install UML Generation Tools

```bash
dotnet tool install --global PlantUmlClassDiagramGenerator
```

# Develop Your Project

## Build, Clean, and Debug

1. Open the integrated terminal in VS Code:
   **View → Terminal** (or **Ctrl + ñ** on a Spanish keyboard).

2. Navigate to the folder containing the project you want to work with (`.csproj`).

3. Build the project. If you are in a project folder, only that project will be compiled. If you are in the solution folder, all projects will be compiled:

```bash
dotnet build
```

To build in Release mode:

```bash
dotnet build -c Release
```

This command:

- Compiles the code
- Restores NuGet packages if necessary
- Generates output under `bin/Debug/net10.0/`

4. Restore packages before building (if there are new dependencies):

```bash
dotnet restore
```

5. Run the application (when located in the Web API folder):

```bash
dotnet run
```

To run a specific project from the solution root:

```bash
dotnet run --project MyApi/MyApi.csproj
```

6. To clean the project or solution:

```bash
dotnet clean
```

## Run Tasks

Press:

```text
Ctrl+Shift+P
```

Then select **Run Task** to execute tasks such as Build, Debug, etc.

## Refactoring

- **Ctrl+Shift+R**: Roslynator-supported refactorings.
- **Ctrl+.**: Generate code, including constructors and `Equals` methods from selected attributes.

# Working with Migrations

In the VS Code integrated terminal, navigate to the **AppForSEII.API** folder containing the model classes and the `.csproj` file:

```bash
cd path/to/your/project/AppForSEII.API
```

### Create a Migration

```bash
dotnet ef migrations add CreateIdentitySchema
```

Creates the migration files in the **Migrations** folder.

### List Existing Migrations

```bash
dotnet ef migrations list
```

### Remove the Last Migration

```bash
dotnet ef migrations remove
```

### Apply the Migration to the Database

```bash
dotnet ef database update
```

### Remove All Applied Migrations from the Database

```bash
dotnet ef database update 0
```

### Delete the Database

```bash
dotnet ef database drop
```

# Generate Diagrams from Code

From the solution folder, run:

```bash
puml-gen ./src/AppForSEII.API/Models ./src/AppForSEII.API/ClassDiagram -dir -excludePaths **/bin,**/obj,**/Migrations -createAssociation -allInOne
```

Command options:

- `-dir`: Processes input/output directories.
- `-excludePaths`: Excludes noise from `bin/` and `obj/`.
- `-createAssociation`: Detects associations from fields and properties.
- `-allInOne`: Creates an `include.puml` file that groups all diagrams.

This generates `.puml` files (and an `include.puml` file when using `-allInOne`) that can be opened and previewed in VS Code using the PlantUML extension.

To view the diagram:

1. Open `include.puml`.
2. Press **Alt+D**.

The PlantUML extension supports rendering through a server (avoiding the need to install Java or Graphviz) and can export diagrams to PNG or SVG.

# Generate an API Client

To generate the API client in the web project:

1. Open a terminal and start the API.
2. Copy the URL of the `swagger.json` file.
3. Open another terminal and navigate to the web project directory:

```bash
cd src
cd AppForSEII.Web
```

4. Run the following command, replacing the Swagger URL as needed:

```bash
nswag openapi2csclient /input:http://localhost:5180/swagger/v1/swagger.json /classname:AppForSEIIAPIClient /namespace:AppForSEII.Web.API /output:AppForSEIIAPIClient.cs
```

# Testing: Using Coverlet Collector

## 1. Add the Coverlet Package

```bash
dotnet add <YOUR_TEST_PROJECT>.csproj package coverlet.collector
```

## 2. Generate Code Coverage

This creates a Cobertura report under:

```text
TestResults/<GUID>/coverage.cobertura.xml
```

Run:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 3. Generate the Coverage Report

```bash
reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html
```

## 4. View Test Coverage

Using the **Coverage Gutters** extension:

```text
Ctrl+Shift+P → Coverage Gutters: Display
```