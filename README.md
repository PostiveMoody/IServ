# IServ
# Краткое описание проекта Figma: https://clck.ru/396dBk
# Техническое задание
Есть API:
http://universities.hipolabs.com/search?country=Russian+Federation
Параметр country позволяет указать страну.
Нужно написать ETL, которая по заранее составленному списку стран (составить список
самостоятельно, количество стран не менее 10), по которым нужно загрузить данные об
учебных заведениях, и также API для публикации загруженных данных, в котором должна быть
реализована фильтрация по стране и названию заведения, данные должны возвращаться в
виде JSON.
Загрузка должна выполняться параллельно, количество потоков определяется настройкой.
Из полученных данных получить: название страны, название заведения, сайт (если сайтов
несколько, то перечислить их через точку с запятой, например:
http://www.site1.ru/;http://www.site2.ru/)
Данные сохранить в таблицу БД. СУБД - любая реляционная на выбор.
В решении должны быть:
1. Решение на C# с кодом ETL и API;
2. Скрипт SQL для создания таблицы;
3. Инструкция с указанием настроек для запуска.

# Инструкция с указанием настроек для запуска
1) Склонировать данный проект: git clone https://github.com/PostiveMoody/IServ.git
2) База данных MSSQl, так что вы должны иметь сервер у себя на комьютере. Либо развернуть image в docker, но в этом случае DockerFile и остальную интеграцию с БД вам придется настроить самостоятельно.
3) Открыть VisualStudio/Package Manager Console и вести туда данную команду: Update-Database

Есть два исполняемых проекта: IServ.API и IServ.ETL. Настройкой для проекта IServ.ETL является файл **appsettings.json**

# Скрипт SQL для создания таблицы
Данный проект содержит всего 3 таблицы и 1 техническую. Таблицы были созданы при помощи подхода Code-First EF.

1) University - таблица учебных заведений с очищенными данными
   
         USE [IServApp]
         GO
         
         /****** Object:  Table [dbo].[University]    Script Date: 09.03.2024 7:41:56 ******/
         SET ANSI_NULLS ON
         GO
         
         SET QUOTED_IDENTIFIER ON
         GO
         
         CREATE TABLE [dbo].[University](
         	[UniversityId] [int] NOT NULL,
         	[Country] [nvarchar](max) NOT NULL,
         	[Name] [nvarchar](max) NOT NULL,
         	[WebPages] [nvarchar](max) NOT NULL,
          CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
         (
         	[UniversityId] ASC
         )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
         ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
         GO
         
         ALTER TABLE [dbo].[University] ADD  DEFAULT (NEXT VALUE FOR [UniversityIdSequence]) FOR [UniversityId]
         GO

2) UniversityRawData - таблица с "сырыми данными"

         USE [IServApp]
         GO
         
         /****** Object:  Table [dbo].[UniversityRawData]    Script Date: 07.03.2024 11:52:33 ******/
         SET ANSI_NULLS ON
         GO
         
         SET QUOTED_IDENTIFIER ON
         GO
         
         CREATE TABLE [dbo].[UniversityRawData](
         	[UniversityRawDataId] [int] NOT NULL,
         	[AlphaTwoCode] [nvarchar](max) NULL,
         	[StateProvince] [nvarchar](max) NULL,
         	[Name] [nvarchar](max) NOT NULL,
         	[Country] [nvarchar](max) NOT NULL,
         	[WebPages] [nvarchar](max) NOT NULL,
         	[Domains] [nvarchar](max) NOT NULL,
          CONSTRAINT [PK_UniversityRawData] PRIMARY KEY CLUSTERED 
         (
         	[UniversityRawDataId] ASC
         )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
         ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
         GO
         
         ALTER TABLE [dbo].[UniversityRawData] ADD  DEFAULT (NEXT VALUE FOR [UniversityRawDataIdSequence]) FOR [UniversityRawDataId]
         GO

3) Country - таблица со странами

         USE [IServApp]
         GO
         
         /****** Object:  Table [dbo].[Country]    Script Date: 11.03.2024 9:40:57 ******/
         SET ANSI_NULLS ON
         GO
         
         SET QUOTED_IDENTIFIER ON
         GO
         
         CREATE TABLE [dbo].[Country](
         	[CountryId] [int] NOT NULL,
         	[Name] [nvarchar](max) NOT NULL,
          CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
         (
         	[CountryId] ASC
         )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
         ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
         GO
         
         ALTER TABLE [dbo].[Country] ADD  DEFAULT (NEXT VALUE FOR [CountryIdSequence]) FOR [CountryId]
         GO

**Так же были созданф sequences**

      USE [IServApp]
      GO
      
      USE [IServApp]
      GO
      
      /****** Object:  Sequence [dbo].[UniversityIdSequence]    Script Date: 29.02.2024 8:02:32 ******/
      CREATE SEQUENCE [dbo].[UniversityIdSequence] 
       AS [int]
       START WITH 1
       INCREMENT BY 1
       MINVALUE 1
       MAXVALUE 100000
       CACHE 
      GO
      
      USE [IServApp]
      GO
      
      USE [IServApp]
      GO
      
      /****** Object:  Sequence [dbo].[UniversityRawDataIdSequence]    Script Date: 07.03.2024 11:54:23 ******/
      CREATE SEQUENCE [dbo].[UniversityRawDataIdSequence] 
       AS [int]
       START WITH 1
       INCREMENT BY 1
       MINVALUE 1
       MAXVALUE 100000
       CACHE 
      GO

      USE [IServApp]
      GO
      
      USE [IServApp]
      GO
      
      /****** Object:  Sequence [dbo].[CountryIdSequence]    Script Date: 11.03.2024 9:41:52 ******/
      CREATE SEQUENCE [dbo].[CountryIdSequence] 
       AS [int]
       START WITH 1
       INCREMENT BY 1
       MINVALUE 1
       MAXVALUE 100000
       CACHE 
      GO
         
# ETL - Extract, Transfer, Load. Один из основных процессов в управлении хранилищами данных.
# С какими задачами поможет ETL

**ETL упрощает процесс работы с информацией за счёт того, что объединяет её из разных источников и решает задачу переноса необработанных и распределённых данных в единый репозиторий. Эти функции ETL полезны во многих процессах.**

1.**Миграция и репликация данных**. Ускорение — одна из главных задач при организации этих двух процессов. Чем быстрее компания перенесет старую информацию в новые системы, тем раньше она сможет ими пользоваться. Иногда это могут быть несовместимые форматы или файлы, а ETL решает эту проблему за счёт преобразования данных.

2.**Сбор и обработка данных**. Как правило, у бизнеса много источников получения информации. Например, данные о продажах и результатах маркетинговых кампаний могут поступать от двух разных сервисов. А проанализировать их в совокупности можно только после их объединения. ETL позволяет сразу перенести данные в нужном формате и делает их подходящими для дальнейшего использования. Тем самым он увеличивает ценность этой информации. Данные загружены, теперь их нужно обработать и проанализировать.

3.**Машинное обучение (ML)**. В озеро данных, как правило, собирается информация из множества источников. Но не вся из неё полезна для дальнейшего обучения алгоритмов. С помощью ETL можно вычленять только нужные данные, преобразовывать в подходящий формат и затем уже загружать в озеро или хранилище.
