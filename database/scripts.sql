-- pass: 123456 = 1TOQlI8cuBomIRsjc585hw==

-- AppUser
create table AppUser
(
    UserId       uniqueidentifier
        constraint PK_AppUser
            primary key,
    UserName     nvarchar(256)                              not null,
    Name         nvarchar(200)                              not null,
    LastName     nvarchar(200)                              not null,
    Email        nvarchar(256)                              not null,
    Password     nvarchar(128)                              not null,
    CreatedDate  datetime
        constraint DF_AppUser_CreatedDate default GETDATE() not null,
    IsActiveFlag bit
        constraint DF_AppUser_IsActiveFlag default 1        not null,
    IsDeleteFlag bit
        constraint DF_AppUser_IsDeleteFlag default 0        not null
)

insert into AppUser (UserId, UserName, Name, LastName, Email, Password)
values (NEWID(), 'admin', 'Super', 'Administrator', 'admin@bsc.com.do', '1TOQlI8cuBomIRsjc585hw==');
insert into AppUser (UserId, UserName, Name, LastName, Email, Password)
values (NEWID(), 'cartiss0', 'Corissa', 'Artiss', 'cartiss0@google.co.uk', '1TOQlI8cuBomIRsjc585hw==');
insert into AppUser (UserId, UserName, Name, LastName, Email, Password)
values (NEWID(), 'ytash1', 'Yul', 'Tash', 'ytash1@clickbank.net', '1TOQlI8cuBomIRsjc585hw==');
insert into AppUser (UserId, UserName, Name, LastName, Email, Password)
values (NEWID(), 'rchomicz2', 'Rochester', 'Chomicz', 'efarriar3@pcworld.com', '1TOQlI8cuBomIRsjc585hw==');
insert into AppUser (UserId, UserName, Name, LastName, Email, Password)
values (NEWID(), 'efarriar3', 'Eleanore', 'Farriar', 'admin@bsc.com.do', '1TOQlI8cuBomIRsjc585hw==');

-- EventType
create table EventType
(
    Id           int identity
        constraint PK_EventType
            primary key
                with (fillfactor = 80),
    Name         nvarchar(100)                                not null,
    CreatedDate  datetime
        constraint DF_EventType_CreatedDate default GETDATE() not null,
    IsActiveFlag bit
        constraint DF_EventType_IsActiveFlag default 1        not null
)

SET IDENTITY_INSERT EventType ON;
insert into EventType (Id, Name)
values (1, N'Inicio de Sesión');
insert into EventType (Id, Name)
values (2, N'Consulta de Usuario');
insert into EventType (Id, Name)
values (3, N'Crear Usuario');
insert into EventType (Id, Name)
values (4, N'Editar Usuario');
insert into EventType (Id, Name)
values (5, N'Eliminar Usuario');
SET IDENTITY_INSERT EventType OFF;


--EventLog
create table EventLog
(
    Id          int identity
        constraint PK_EventLog
            primary key
                with (fillfactor = 80),
    UserId      uniqueidentifier                             not null
        constraint FK_EventLog_AppUser
            references AppUser,
    EventTypeId int                                          not null
        constraint FK_EventLog_EventType
            references EventType,
    EventDate   datetime
        constraint DF_EventLog_EventDate default GETDATE()   not null,
    Payload     nvarchar(500)                                not null,
    CreatedDate datetime
        constraint DF_EventLog_CreatedDate default GETDATE() not null,
)




