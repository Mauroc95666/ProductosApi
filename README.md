# Productos API — Segundo Parcial Programación Web II

Módulo **Productos** desarrollado con **ASP.NET Core Web API 8**, **Entity Framework Core**, **SQL Server** y **Swagger**. Todas las operaciones se realizan contra SQL Server mediante EF Core (no hay listas locales ni datos simulados).

## Tecnologías

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core 8 (SqlServer, Tools, Design)
- SQL Server (`Macrosboy\SQL_DEV`)
- Swagger (Swashbuckle.AspNetCore)

## Estructura del modelo `Producto`

| Campo       | Tipo    | Descripción                          |
|-------------|---------|--------------------------------------|
| Id          | int     | Clave primaria (autoincremental)     |
| Nombre      | string  | Obligatorio, máx. 100 caracteres     |
| Descripcion | string? | Opcional, máx. 250 caracteres        |
| Precio      | decimal | Mayor a 0, decimal(18,2)             |
| Stock       | int     | Mayor o igual a 0                    |

## Cadena de conexión

En `appsettings.json`:

```json
"Server=Macrosboy\\SQL_DEV;Database=ProductosDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

> Si tu instancia usa usuario/contraseña en lugar de autenticación de Windows, cámbiala por:
> `Server=Macrosboy\SQL_DEV;Database=ProductosDB;User Id=sa;Password=TU_PASSWORD;TrustServerCertificate=True;`

## Cómo ejecutar (paso a paso)

Requisitos: **.NET 8 SDK** y **SQL Server** en ejecución.

```bash
# 1. Restaurar paquetes
dotnet restore

# 2. Instalar la herramienta de EF (si no la tienes)
dotnet tool install --global dotnet-ef

# 3. Crear la migración inicial
dotnet ef migrations add InitialCreate

# 4. Crear/actualizar la base de datos en SQL Server
dotnet ef database update

# 5. Ejecutar la API
dotnet run
```

Al ejecutar, abre en el navegador: **http://localhost:5090/swagger**

## Endpoints implementados

| # | Método | Ruta                              | Descripción                                        |
|---|--------|-----------------------------------|----------------------------------------------------|
| 2 | GET    | `/api/productos`                  | Lista todos los productos                          |
| 3 | GET    | `/api/productos/{id}`             | Obtiene un producto; **404** si no existe          |
| 4 | POST   | `/api/productos`                  | Registra un producto (con validación)              |
| 5 | PUT    | `/api/productos/{id}`             | Actualiza un producto existente                    |
| 6 | DELETE | `/api/productos/{id}`             | Elimina un producto existente                      |
| 7 | GET    | `/api/productos/buscar?nombre=`   | Búsqueda por coincidencia parcial (lista vacía si no hay) |

### Ejemplo de cuerpo para POST / PUT

```json
{
  "nombre": "Teclado mecánico",
  "descripcion": "Switches azules RGB",
  "precio": 250.50,
  "stock": 15
}
```

## 8. Evidencias de prueba en Swagger

> Reemplaza estos textos por tus capturas de pantalla. Colócalas en una carpeta
> `evidencias/` y enlázalas así: `![GET](evidencias/get.png)`

- [ ] **GET** `/api/productos` — captura mostrando la lista.
- [ ] **GET por ID** `/api/productos/1` — captura del producto encontrado.
- [ ] **GET por ID inexistente** `/api/productos/999` — captura mostrando **404**.
- [ ] **POST** — captura del producto creado (respuesta 201).
- [ ] **PUT** — captura del producto actualizado.
- [ ] **DELETE** — captura de eliminación correcta.
- [ ] **Buscar** `/api/productos/buscar?nombre=tec` — captura con coincidencias parciales.

## Base de datos

Adjuntar el respaldo de la base de datos (`ProductosDB.bak`) o el script de creación
generado por la migración (carpeta `Migrations/`).

## 9. Git y GitHub

```bash
git init
git add .
git commit -m "Estructura inicial del proyecto Productos API con EF Core y SQL Server"

# (luego de probar / agregar evidencias)
git add .
git commit -m "CRUD completo, endpoint de búsqueda y evidencias en Swagger"

git branch -M main
git remote add origin https://github.com/TU_USUARIO/productos-api.git
git push -u origin main
```

Compartir el enlace del repositorio en el campus.
