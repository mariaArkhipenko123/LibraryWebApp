# LibraryWebApp
Разработка CRUD Web API для имитации библиотеки (создание, изменение, удаление, получение), выполняется на .Net Core, с использованием EF Core.

Пример инструкции по запуску проекта "Web API Library с Identity Server" и "React приложения в Visual Studio Code":

Описание проекта: Проект состоит из двух частей: Web.api Library, который предоставляет доступ к библиотеке книг, и React приложения для взаимодействия и автогенерация клиента для Web API. Для аутентификации и авторизации используется Identity Server.

Требования к системе:

Операционная система: Windows 10 или выше Visual Studio Code установленный на вашем компьютере Node.js версии 12 или выше

Установка и настройка Web API Library с Identity Server:
Склонируйте репозиторий Web API Library и Identity Server: gh repo clone mariaArkhipenko123/LibraryWebApp 
Перейдите в директорию проекта: cd web-api-library Установите необходимые зависимости: dotnet restore 
Настройте файлы конфигурации для подключения к базе данных и Identity Server. 
Запустите проектЫ Library.sln (Library.WebApi) и Library.Identity в Visual Studio

Установка и запуск React приложения в Visual Studio Code: Откройте Visual Studio Code и откройте папку с проектом React. Установите необходимые зависимости: npm install Настройте файлы конфигурации для указания URL API Library и Identity Server. Запустите React приложение: npm start
