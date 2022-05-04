-- auto-generated definition
create table CAT_CatalogoGeneral
(
    Id     int          not null constraint PK_CAT_CatalogoGeneral primary key,
    Nombre varchar(259) not null,
    Tipo   varchar(50)  not null
);
go  
-- auto-generated definition
create table CAT_Usuarios
(
    UsuarioId                 int			not null constraint PK_Usuario primary key,
    Nombre                    varchar(255)  not NULL,
    Username                  varchar(55)   not NULL,
    Password                  varchar(8)    not NULL,
    Enabled					  bit			not NULL
)
go
-- auto-generated definition
create table CAT_Doctores
(
    DoctorId                  int			not null constraint PK_Doctor primary key,
    Nombre                    varchar(255)  not NULL, 
    Enabled					  bit			not NULL
)
go
-- auto-generated definition
create table CAT_Estatus
(
    Id     int         not null constraint PK_CAT_Estatus primary key,
    Nombre varchar(50) not null,
    Tipo   varchar(50) not null
)
go
 
CREATE TABLE [dbo].[CIT_Citas](
	[CitaId] [int]    NOT NULL	  constraint PK_Citas primary key,
	[DoctorId] [int]  NOT NULL    constraint FK__CAT_Doctores references CAT_Doctores,
	[UsuarioId] [int] NULL		  constraint FK__CAT_Usuarios references CAT_Usuarios,
	[FechaCita] [datetime]	 NOT NULL,
	[HoraCita]  [int]		 NOT NULL, 
	[Paciente] [varchar](55) NOT NULL,
	[CorreoElectronico] [varchar](55) NOT NULL,  
	[EstatusId] [int]  NOT NULL constraint FK__CAT_Estatus default(1) references CAT_Estatus,
	[AuditUser] [varchar](50) NULL,
	[AuditDate] [datetime]	  NULL
)
go
 
go
 
INSERT INTO CAT_Estatus (Id, Nombre, Tipo) VALUES (1, N'Reservada', N'Citas'); 
INSERT INTO CAT_Estatus (Id, Nombre, Tipo) VALUES (2, N'Cancelada', N'Citas');
 					 
INSERT INTO CAT_Usuarios(UsuarioId, Nombre, Username, Password, Enabled) VALUES (1,'admin','admin@citas.com','123', 1)
INSERT INTO CAT_Usuarios(UsuarioId, Nombre, Username, Password, Enabled) VALUES (2,'demo','demo@citas.com','123', 1)


INSERT INTO CAT_Doctores(DoctorId, Nombre, Enabled) VALUES (1,'Dr. Pedro Martínez Olvera', 1)
INSERT INTO CAT_Doctores(DoctorId, Nombre, Enabled) VALUES (2,'Dr. Jaime Coss Méndez', 1)
INSERT INTO CAT_Doctores(DoctorId, Nombre, Enabled) VALUES (3,'Dr. Patricio Almaguer Pardo', 1)


