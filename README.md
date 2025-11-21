# Onion Architecture Template
## What & Why?
- This solution is designed to facilitate the [Onion Architecture] implementation.
- The Solution Contains Structure folders and projects (Layers) and dependency configured based on [Onion Architecture].
- You can clone the repository and start coding without having to implement the design pattern yourself.
- This solution contains the [Repository & Services configurations] with the [Repository & Service Managers] implementation, yes with example.
- This solution Implements Lazy loading when accessing the Repositories and Services for best performance.
## What if I'm new to Onion Architecture?
- It's Okey don't worry, Each project (layer) has a file (Notes.txt) witch contains instructions & specification for
  - what the layer referenced (depending on)
  - what each class does
  - what are the layer responsibilities
## What the layers contain?
- [Core] folder:
  - [Domain] project (layer)
    - Has 3 folders by default (and can be extended with more)
    - This project does NOT depend on any other project except (Shared), but outter projects can depend on this project such as [Services], [Persistent].
      - [Entities] Folder
	      - This folder contains the business objects of the application. These are the core classes that represent the data and behavior of the application.
	      - Example: [`User`, `Product`, `Order`]
	      - These classes are the table/s representations in the database that may be translated to.
      - [Exceptions] Folder
	      -  This folder contains custom exception classes that are specific to the application's domain.
	      - Example: [`UserNotFoundException`, `InvalidOrderException`]
	      - These exceptions help in handling errors in a way that is meaningful to the business logic.]
	      - This folder may also subdivide into more folders such as (UserExceptions, ProductExceptions, etc) if there are many exceptions.
    - [RepositoryInterfaces] Folder
	    - This folder contains interfaces that define the contract for data access operations.
	    - Example: [`IUserRepository`, `IProductRepository`, `IOrderRepository`]
	    - These interfaces abstract the data access layer, allowing for different implementations (e.g., in-memory, database, etc.) without changing the business logic.
	    - This folder may also subdivide into more folders such as (UserRepositories, ProductRepositories, etc) if there are many repository interfaces.

  - [Service.Abstraction] layer
    - This project contains the service layer interfaces for the application.
    - This project does NOT depend on any other project except (Shared), but outer projects can depend on this project such as [Services], [Presentation].
    - This project may subdivide into folders if needed.
    - This project contains the [IServiceManager] interface which is responsible for managing all service interfaces and accessing them.

  - [Services] layer
    - This project depends on the [Service.Abstraction, Domain] projects.
    - This peoject is securely isolated (NO other project depend on it).
    - This project contains the implementations for [Service.Abstraction] layer interfaces.
    - This layer can be subdivided into many folders if needed.
    - e.g. UserServices, ProductServices, etc. 
    - This project also contains the [ServiceManager] class which is responsible for managing all services and accessing them.
    - The [ServiceManager] class implements the [IServiceManager] interface.
    - The [ServiceManager] class uses the lazy loading for best preformance.
  
- [Infrastructure] folder:
  - [Persistent] layer
    - This project contains the [RepositoryDbContext] class which is responsible for the database connection and enities resjestration.
    - This project is securely isolated (NO other project depend on it) so its implementation is protected.
    - This project contains the implementations for the repository interfaces which defined at [Domine] layer.
    - This project depends on the [Domain] project only.
  	  - To access the repository interfaces.
  	  - To access the entity classes.
  	  - To access the custom exceptions classes.

    - [Configuration] folder:
	    - This folder contains the entity configurations classes.
	    - e.g. UserConfiguration, ProductConfiguration, etc.
	    - Each configuration class implements the IEntityTypeConfiguration<T> interface from the EF Core.
	    - Each configuration class is responsible for configuring its corresponding entity from the [Domain] layer.

    - [Repositories] folder:
	    - This folder contains the implementation of each repository interface.
	    - e.g. UserRepository, ProductRepository, etc.
	    - Each repository class implements its corresponding interface from the [Domain] layer.
	    - The [RepositoryManager] class implements the [IRepositoryManager] interface.
	    - This folder contains the [RepositoryManager] class which is responsible for managing all repository interfaces and accessing them.
	    - The [RepositoryManager] class uses the lazy loading for best preformance.]

    - [Migrations] folder:
	    - This folder contains the EF Core migrations files.
	    - Each migration file is responsible for applying the changes to the database schema.

  - [Presentation] layer
    - This project contains the [Controllers] folder which contains all the API controllers for the application.]
	  - this folder can be subdivided into many folders if needed.
    - This project depends only on the [Service.Abstraction] project layer.
    - This project contains [AssemblyReference] class which is used in the [API] layer for in order to use the controller from other project.
- [API] layer
  - This is the startup project (the entry) for the application.
  - This project contains the [Program.cs] file which is responsible for configuring and starting the application.
  - this project depends (references) all other projects in order to be used in the dependincey injection.
	  - this includes [Repository, Services] Interfaces/Abstractions and their implementations at [Services, Persistant] layers.

  - [Program.cs] file:
  	- This file where we register all the services to their corresponding Interfaces needed for the application.
  	- This file where we register all the repositories to their corresponding Interfaces.
  	- This file is responsible for configuring the application services and middleware.
  	- This file is responsible for configuring the database connection string.
  	- This file is responsible for configuring the logging services.
  	- This file is responsible for configuring the exception handling middleware.
  	- This file is responsible for configuring the swagger middleware.
  	- This file is responsible for configuring the CORS policy.
  	- This file is responsible for configuring the authentication and authorization middleware.
  	- This file is responsible for seeding the database with initial data.]

  - This layer contains the [Middelware] folder:
  	- for configurations and custom middelware implementation for the application.
  - This layer contains the [Extensions] folder:
  	- for extension methods for the application.
  - This layer contains the [ActionFilters] folder:
  	- for custom action filters implementation for the application.
  - This layer contains the database connection string in the [appsettings.json] file (Enviroment Secrets for true protection).
  
- [Shared] layer
  - This layer contains the shared classes for the application such as (enums, constants, etc...).
  - This layer does NOT depend on any project but all can depend on it.
  - This layer classes manily used at [Services, Service.Abstraction, Presentation] layers.
  - This layer contains the [Dtos] folder which contains all the Data Transfer Objects for the application.
