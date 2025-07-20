# Vending Machine API (Автомат с газированными напитками)

Данный проект был разработан Григорьевым Данилом Валерьевичем в качестве тестового задания для компании ООО «Интравижн».

## Сервер ASP NET CORE

### Стек
Для разработки сервера была выбранна архитектура Clean Architecture, а также выбран подход DDD для создания доменного слоя. 

Использованные библиотеки:
- Fluent Validation;
- Entity Framework;
- Closed XML;
- CSharpFunctionalExtensions;
- Scrutor;
  
Были реализованы паттерны: 
- Repository;
- Factory Method;
- Result;
- CQRS;
- UnitOfWork;

### Импорт данных

В корне проекта есть файл insert_records.sql который представляет собой запрос на создание брендов и монет, а также excel таблица для проверки вставки товаров.

### ВАЖНО!

Для сервера не выделен домен, поэтому для работы с клиентом запускайте проект с профилем http или же меняйте BASE_URL на клиенте по пути src\shared\api\constants.ts

### Возможные проблемы

Вас возможно будет перекидывать на страницу /isBusy даже если автомат не занят, это баг который я не успел решить, просто переходите на главную страницу с помощью кнопки "Каталог напитков"

### Endpoints

<img width="943" height="1165" alt="image" src="https://github.com/user-attachments/assets/d1867461-dc33-4758-b190-adb0cb95d417" />

## База данных (PostgreSQL) и докер контейнер

## Даталогическая схема

<img width="756" height="986" alt="image" src="https://github.com/user-attachments/assets/0cd9ff17-b85e-4559-8cb7-859efb1d5c02" />

## О докере

В папке сервера есть docker-compose файл для использования бд и pgAdmin

## Клиент Next js

### Стек
Для разработки клиента была выбранна архитектура FSD. Для создания стилей использовался Tailwind CSS.

Использованные библиотеки:
- Redux Toolkit;
- RTK Query;
- Zod;
- MUI;
- Notistack;
- CLSX;

### Навигация

<img width="1601" height="761" alt="image" src="https://github.com/user-attachments/assets/cb91dffd-81ab-42ba-ae04-5c0e198f75f1" />

### Установка

```bash
npm i
npm run build
npm start
```

## Выполненные требования
В соответсвии с ТЗ были выполненны следующие требования:
1. С помощью middleware на сервере был реализовано использование автомата только 1-м человеком;
2. Был создан endpoint для импорта товаров(напитков) из Excel таблицы;
