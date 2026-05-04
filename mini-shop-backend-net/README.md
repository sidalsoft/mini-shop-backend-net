# 🛒 Mini Shop Backend (.NET)

Backend API для мини интернет-магазина с поддержкой аутентификации, корзины, заказов и админ-панели.

---

## 🚀 Технологии

* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **PostgreSQL**
* **JWT Authentication**
* **FluentValidation**
* **Swagger (OpenAPI)**
* **Docker & Docker Compose**

---

## 📦 Функциональность

### 🔐 Аутентификация

* Регистрация пользователя
* Авторизация (JWT)
* Роли: `USER`, `ADMIN`
* Автоматическое создание admin при старте

---

### 🛍️ Товары

* CRUD (только ADMIN)
* Фильтрация (по имени, цене)
* Сортировка
* Пагинация

---

### 🗂️ Категории

* CRUD (ADMIN)
* Связь с товарами

---

### 🛒 Корзина

* Добавление товара
* Изменение количества
* Удаление товара
* Очистка корзины

---

### 📦 Заказы

* Создание заказа из корзины
* Snapshot данных (цена фиксируется)
* История заказов пользователя
* Просмотр заказов (ADMIN)
* Фильтрация и пагинация

---

### ⚙️ Дополнительно

* Глобальный error handler
* Валидация через FluentValidation
* Swagger с JWT авторизацией
* Seed admin при первом запуске
* Docker окружение

---

## 🧱 Архитектура

Проект построен с разделением на слои:

```
Application/
  Services
  DTOs
  Validators

Infrastructure/
  DbContext
  Repositories
  Middleware

Domain/
  Entities
```

---

## 🔐 Авторизация в Swagger

1. Выполнить login
2. Скопировать JWT token
3. Нажать **Authorize**
4. Вставить:

```
Bearer {your_token}
```

---

## ⚙️ Запуск проекта

### 🔹 Локально

```bash
dotnet restore
dotnet ef database update
dotnet run
```

---

### 🐳 Docker

```bash
docker compose up --build
```

---

## 🔑 Переменные окружения

Пример:

```
JWT_KEY=your-secret-key
ADMIN_EMAIL=admin@gmail.com
ADMIN_PASSWORD=admin
DB_CONNECTION=Host=db;Port=5432;Database=mini_shop_net_db;Username=postgres;Password=postgres
```

---

## 🧪 Миграции

Миграции применяются автоматически при старте приложения:

```csharp
db.Database.Migrate();
```

---

## 📡 API Примеры

### Регистрация

```
POST /api/auth/register
```

### Логин

```
POST /api/auth/login
```

### Товары

```
GET /api/products
POST /api/products (ADMIN)
```

### Корзина

```
POST /api/cart
GET /api/cart
```

### Заказы

```
POST /api/orders
GET /api/orders/my
```

---

## 📈 Улучшения (roadmap)

* Refresh Token
* Order status lifecycle
* Logging (Serilog)
* Unit / Integration tests
* Stock & availability
* Payment integration

---

## 👨‍💻 Автор

Saidali Saburov
Senior Backend Developer

---

## ⭐ Оценка проекта

* Архитектура: ✔️
* CRUD: ✔️
* Безопасность: ✔️
* Production-ready: частично ✔️

---
