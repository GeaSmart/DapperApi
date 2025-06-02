# 🛠️ Dapper API Example – .NET 9

Este proyecto demuestra cómo construir una API REST con .NET 9 y Dapper, permitiendo mapear relaciones uno-a-muchos (por ejemplo, `Product` con sus `Tags`) y devolver respuestas JSON con objetos anidados.

## 🚀 Características

* ASP.NET Core 8
* Uso de Dapper como micro ORM
* Patrón Repository
* Consultas SQL manuales
* Respuestas JSON con relaciones anidadas (uno a muchos)
* Estructura limpia y orientada a buenas prácticas

---

## 🧱 Estructura del proyecto

* `Controllers/`: Endpoints de la API
* `Repositories/`: Acceso a datos con Dapper
* `DTOs/`: Objetos de transferencia de datos
* `Entities/`: Entidades de dominio

---

## 🥪 Ejemplo de respuesta JSON (del endpoint con data anidada)

```json
{
  "id": "guid-del-producto",
  "description": "Laptop Dell XPS 13",
  "price": 1299.99,
  "tags": [
    { "id": 1, "name": "Electrónica" },
    { "id": 2, "name": "Portátiles" }
  ]
}
```

---

## 🗒️ Script para crear la base de datos

Puedes ejecutar este script en SQL Server para crear la base de datos y poblarla con datos de prueba:

```sql
-- Crear la base de datos
CREATE DATABASE DapperApiDb;
GO

-- Usar la base de datos
USE DapperApiDb;
GO

-- Crear tabla Products
CREATE TABLE Products (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Description VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

-- Crear tabla Tags
CREATE TABLE Tags (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Insertar datos de prueba
DECLARE @Id1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Id2 UNIQUEIDENTIFIER = NEWID();
DECLARE @Id3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Products (Id, Description, Price) VALUES
(@Id1, 'Laptop Dell XPS 13', 1299.99),
(@Id2, 'Teclado Mecánico RGB', 89.50),
(@Id3, 'Monitor 4K LG 27"', 399.00);

INSERT INTO Tags (Name, ProductId) VALUES
('Electrónica', @Id1),
('Portátiles', @Id1),
('Accesorios', @Id2),
('Gaming', @Id2),
('Pantallas', @Id3);

-- Consultar resultados
SELECT * FROM Products;
SELECT * FROM Tags;
```

---

## ⚙️ Configuración

1. Clona este repositorio:

   ```bash
   git clone https://github.com/GeaSmart/DapperApi.git
   ```

2. Configura la cadena de conexión en `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=DapperApiDb;integrated security=true;trust server certificate=true"
   }
   ```

3. Ejecuta el proyecto:

   ```bash
   dotnet run
   ```

---

## 📬 Contacto

Hy dev, este proyecto fue desarrollado para fines educativos y de demostración, sin embargo el código tiene calidad y buenas prácticas.
¿Tienes sugerencias o mejoras? ¡Contribuciones bienvenidas!
[Visita mi blog en BraveDeveloper](https://bravedeveloper.com/blog)


---
