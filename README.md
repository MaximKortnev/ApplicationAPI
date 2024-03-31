# Инструкция по запуску сервиса
Данный репозиторий содержит API для работы с заявками для участия в конференции. Для запуска сервиса выполните следующие шаги:

## Предварительные требования
1. **.NET 8 SDK with ASP.net Core** 
2. **PostgreSQL 16**
3. **EFCore**
4. **NpgSql**
5. **Swagger**

### Шаги по установке и запуску
1. **Клонирование репозитория**

    *git clone https://github.com/MaximKortnev/ApplicationAPI.git*

2. **Переход в директорию проекта**
   
    *cd your-repository*
> Замените your-repository на название вашего репозитория.

3. **Настройка подключения к базе данных**

> В файле appsettings.json укажите строки подключения к вашей PostgreSQL базе данных:

*"ConnectionStrings": {
    "DefaultConnection": "Your_PostgreSQL_Connection_String_Here"
}*

4. **Запуск проекта**

*dotnet run*

> После этого API будет доступен по адресу > После этого API будет доступен по адресу https://localhost:7287/swagger/index.html.

# API Endpoints
- Создание заявки: POST /Application/CreateApplication
- Редактирование заявки: PUT /Application/{id}
- Удаление заявки: DELETE /Application/{id}
- Отправка на рассмотрение: POST /Application/{id}/submit
- Получение заявок после даты: GET /Application?submittedAfter={submittedAfter}
- Получение не поданных заявок: GET /Application?unsubmittedOlder={unsubmittedOlder}
- Получение текущей заявки пользователя: GET /Application/users/{userId}/currentapplication
- Получение заявки по ID: GET /Application/{id}
- Получение списка активностей: GET /Application/activities

