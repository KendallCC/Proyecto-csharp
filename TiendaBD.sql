USE [master]
GO
/****** Object:  Database [Tienda]    Script Date: 29/11/2023 16:11:26 ******/
CREATE DATABASE [Tienda]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tienda', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Tienda.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Tienda_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Tienda_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Tienda] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tienda].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tienda] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tienda] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tienda] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tienda] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tienda] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tienda] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tienda] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tienda] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tienda] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tienda] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tienda] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tienda] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tienda] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tienda] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tienda] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Tienda] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tienda] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tienda] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tienda] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tienda] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tienda] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tienda] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tienda] SET RECOVERY FULL 
GO
ALTER DATABASE [Tienda] SET  MULTI_USER 
GO
ALTER DATABASE [Tienda] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tienda] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tienda] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tienda] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Tienda] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Tienda] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Tienda', N'ON'
GO
ALTER DATABASE [Tienda] SET QUERY_STORE = OFF
GO
USE [Tienda]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[IDCarrito] [int] IDENTITY(1,1) NOT NULL,
	[IDUsuario] [int] NULL,
	[IDProducto] [int] NULL,
	[Cantidad] [int] NULL,
	[Total] [decimal](10, 2) NULL,
 CONSTRAINT [PK__CarritoD__C8EB8D92B50627E6] PRIMARY KEY CLUSTERED 
(
	[IDCarrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[IDCategoria] [int] IDENTITY(1,1) NOT NULL,
	[NombreCategoria] [nvarchar](100) NOT NULL,
	[DescripcionCategoria] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Categori__70E82E28E77AFE08] PRIMARY KEY CLUSTERED 
(
	[IDCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallePedido]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallePedido](
	[IDDetallePedido] [int] IDENTITY(1,1) NOT NULL,
	[IDPedido] [int] NULL,
	[IDProducto] [int] NULL,
	[Cantidad] [int] NULL,
	[PrecioUnitario] [decimal](10, 2) NULL,
	[Subtotal] [decimal](10, 2) NULL,
 CONSTRAINT [PK__Detalles__8F05524483100255] PRIMARY KEY CLUSTERED 
(
	[IDDetallePedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoPedido]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoPedido](
	[EstadoID] [int] IDENTITY(1,1) NOT NULL,
	[Detalle] [nvarchar](50) NULL,
 CONSTRAINT [PK__EstadosD__FEF86B6014F7C974] PRIMARY KEY CLUSTERED 
(
	[EstadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[IDFactura] [int] IDENTITY(1,1) NOT NULL,
	[IDPedido] [int] NULL,
	[FechaFactura] [date] NULL,
	[DetallesFactura] [nvarchar](max) NULL,
 CONSTRAINT [PK__Facturas__492FE9393D266844] PRIMARY KEY CLUSTERED 
(
	[IDFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialPedido]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialPedido](
	[IDHistorialPedidos] [int] IDENTITY(1,1) NOT NULL,
	[IDUsuario] [int] NULL,
	[IDPedido] [int] NULL,
 CONSTRAINT [PK__Historia__3ADC4F23C45631CA] PRIMARY KEY CLUSTERED 
(
	[IDHistorialPedidos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[IDPedido] [int] IDENTITY(1,1) NOT NULL,
	[IDUsuario] [int] NULL,
	[FechaPedido] [date] NULL,
	[EstadoID] [int] NULL,
 CONSTRAINT [PK__Pedidos__00C11F9957A41C7E] PRIMARY KEY CLUSTERED 
(
	[IDPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[IDProducto] [int] IDENTITY(1,1) NOT NULL,
	[NombreProducto] [nvarchar](255) NOT NULL,
	[DescripcionProducto] [nvarchar](255) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[IDCategoria] [int] NOT NULL,
	[Imagen] [nvarchar](255) NULL,
 CONSTRAINT [PK__Producto__ABDAF2B456D402C3] PRIMARY KEY CLUSTERED 
(
	[IDProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolUsuario]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolUsuario](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [nvarchar](50) NULL,
 CONSTRAINT [PK__RolesDeU__8AFACE3A0D90CAFE] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 29/11/2023 16:11:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IDUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
	[CorreoElectronico] [nvarchar](255) NOT NULL,
	[Contraseña] [nvarchar](255) NULL,
	[RoleID] [int] NOT NULL,
	[Estado] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK__Usuarios__52311169B70A93EF] PRIMARY KEY CLUSTERED 
(
	[IDUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Carrito] ON 

INSERT [dbo].[Carrito] ([IDCarrito], [IDUsuario], [IDProducto], [Cantidad], [Total]) VALUES (27, 2, 3, 2, CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[Carrito] ([IDCarrito], [IDUsuario], [IDProducto], [Cantidad], [Total]) VALUES (28, 2, 2, 1, CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[Carrito] ([IDCarrito], [IDUsuario], [IDProducto], [Cantidad], [Total]) VALUES (29, 2, 4, 1, CAST(300000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Carrito] OFF
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([IDCategoria], [NombreCategoria], [DescripcionCategoria]) VALUES (1, N'Tecnologia', N'Las Mejores Computadoras Solamente en TechStore')
INSERT [dbo].[Categoria] ([IDCategoria], [NombreCategoria], [DescripcionCategoria]) VALUES (2, N'Accesorios', N'Los Mejores Accesorios para tu pc solamente en TechStore')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[DetallePedido] ON 

INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (1, 2, 3, 1, CAST(15000.00 AS Decimal(10, 2)), CAST(15000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (2, 2, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (3, 2, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (4, 3, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (5, 3, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (6, 3, 3, 1, CAST(150000.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (7, 4, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (8, 5, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (9, 6, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (10, 6, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (11, 6, 3, 1, CAST(150000.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (12, 6, 4, 1, CAST(300000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (13, 7, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (14, 7, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (15, 8, 3, 1, CAST(150000.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (16, 8, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (17, 9, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (18, 9, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (19, 10, 4, 1, CAST(300000.00 AS Decimal(10, 2)), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (20, 10, 3, 1, CAST(150000.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (21, 11, 3, 1, CAST(150000.00 AS Decimal(10, 2)), CAST(150000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (22, 11, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (23, 11, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (24, 12, 4, 5, CAST(300000.00 AS Decimal(10, 2)), CAST(1500000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (25, 13, 1, 1, CAST(125000.00 AS Decimal(10, 2)), CAST(125000.00 AS Decimal(10, 2)))
INSERT [dbo].[DetallePedido] ([IDDetallePedido], [IDPedido], [IDProducto], [Cantidad], [PrecioUnitario], [Subtotal]) VALUES (26, 13, 2, 1, CAST(50000.00 AS Decimal(10, 2)), CAST(50000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[DetallePedido] OFF
GO
SET IDENTITY_INSERT [dbo].[EstadoPedido] ON 

INSERT [dbo].[EstadoPedido] ([EstadoID], [Detalle]) VALUES (1, N'Pendiente')
INSERT [dbo].[EstadoPedido] ([EstadoID], [Detalle]) VALUES (2, N'Entregado')
SET IDENTITY_INSERT [dbo].[EstadoPedido] OFF
GO
SET IDENTITY_INSERT [dbo].[Factura] ON 

INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (1, 2, CAST(N'2023-11-17' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (2, 3, CAST(N'2023-11-20' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (3, 4, CAST(N'2023-11-20' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (4, 5, CAST(N'2023-11-20' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (5, 6, CAST(N'2023-11-20' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (6, 7, CAST(N'2023-11-25' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (7, 8, CAST(N'2023-11-25' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (8, 9, CAST(N'2023-11-25' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (9, 10, CAST(N'2023-11-25' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (10, 11, CAST(N'2023-11-25' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (11, 12, CAST(N'2023-11-25' AS Date), N'Pago con tarjeta')
INSERT [dbo].[Factura] ([IDFactura], [IDPedido], [FechaFactura], [DetallesFactura]) VALUES (12, 13, CAST(N'2023-11-29' AS Date), N'Pago con tarjeta')
SET IDENTITY_INSERT [dbo].[Factura] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedido] ON 

INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (2, 1, CAST(N'2023-11-17' AS Date), 2)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (3, 2, CAST(N'2023-11-20' AS Date), 2)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (4, 2, CAST(N'2023-11-20' AS Date), 2)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (5, 2, CAST(N'2023-11-20' AS Date), 2)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (6, 2, CAST(N'2023-11-20' AS Date), 2)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (7, 2, CAST(N'2023-11-25' AS Date), 2)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (8, 2, CAST(N'2023-11-25' AS Date), 1)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (9, 2, CAST(N'2023-11-25' AS Date), 1)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (10, 2, CAST(N'2023-11-25' AS Date), 1)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (11, 2, CAST(N'2023-11-25' AS Date), 1)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (12, 2, CAST(N'2023-11-25' AS Date), 1)
INSERT [dbo].[Pedido] ([IDPedido], [IDUsuario], [FechaPedido], [EstadoID]) VALUES (13, 2, CAST(N'2023-11-29' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Pedido] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([IDProducto], [NombreProducto], [DescripcionProducto], [Precio], [IDCategoria], [Imagen]) VALUES (1, N'Computadora Laptop Hp', N'Computadora HP 4GB ram 128SSD intel core i3 con lector de huellas', CAST(125000.00 AS Decimal(10, 2)), 1, N'/Product-images/Computadora_.jpg')
INSERT [dbo].[Producto] ([IDProducto], [NombreProducto], [DescripcionProducto], [Precio], [IDCategoria], [Imagen]) VALUES (2, N'Disco SSD', N'Disco SSD de 2 TB', CAST(50000.00 AS Decimal(10, 2)), 2, N'/Product-images/SSD_.jpg')
INSERT [dbo].[Producto] ([IDProducto], [NombreProducto], [DescripcionProducto], [Precio], [IDCategoria], [Imagen]) VALUES (3, N'Tarjeta grafica nvidia RTX', N'Tarjeta Nvidia RTX 3060 12GB', CAST(150000.00 AS Decimal(10, 2)), 2, N'/Product-images/TarjetaGrafica_.jpg')
INSERT [dbo].[Producto] ([IDProducto], [NombreProducto], [DescripcionProducto], [Precio], [IDCategoria], [Imagen]) VALUES (4, N'PC Gamer master', N'PC con RTX 3060ti 16gb ram i7 11400k', CAST(300000.00 AS Decimal(10, 2)), 1, N'/Product-images/Tecnologia2_.jpg')
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[RolUsuario] ON 

INSERT [dbo].[RolUsuario] ([RoleID], [Rol]) VALUES (1, N'Administrador')
INSERT [dbo].[RolUsuario] ([RoleID], [Rol]) VALUES (2, N'Empleados')
INSERT [dbo].[RolUsuario] ([RoleID], [Rol]) VALUES (3, N'Cliente')
SET IDENTITY_INSERT [dbo].[RolUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([IDUsuario], [Nombre], [CorreoElectronico], [Contraseña], [RoleID], [Estado]) VALUES (1, N'Andrey Jimenez Porras', N'rajimenezp35@gmail.com', N'Andrey123456', 2, N'Activo')
INSERT [dbo].[Usuario] ([IDUsuario], [Nombre], [CorreoElectronico], [Contraseña], [RoleID], [Estado]) VALUES (2, N'Kendall Cespedes', N'Kendall26092002@gmail.com', N'Kendall2626', 1, N'Activo')
INSERT [dbo].[Usuario] ([IDUsuario], [Nombre], [CorreoElectronico], [Contraseña], [RoleID], [Estado]) VALUES (3, N'Cliente', N'Cliente@gmail.com', N'Cliente2626', 3, N'Activo')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK__CarritoDe__IDPro__30F848ED] FOREIGN KEY([IDProducto])
REFERENCES [dbo].[Producto] ([IDProducto])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK__CarritoDe__IDPro__30F848ED]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK__CarritoDe__IDUsu__300424B4] FOREIGN KEY([IDUsuario])
REFERENCES [dbo].[Usuario] ([IDUsuario])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK__CarritoDe__IDUsu__300424B4]
GO
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD  CONSTRAINT [FK__DetallesD__IDPed__37A5467C] FOREIGN KEY([IDPedido])
REFERENCES [dbo].[Pedido] ([IDPedido])
GO
ALTER TABLE [dbo].[DetallePedido] CHECK CONSTRAINT [FK__DetallesD__IDPed__37A5467C]
GO
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD  CONSTRAINT [FK__DetallesD__IDPro__38996AB5] FOREIGN KEY([IDProducto])
REFERENCES [dbo].[Producto] ([IDProducto])
GO
ALTER TABLE [dbo].[DetallePedido] CHECK CONSTRAINT [FK__DetallesD__IDPro__38996AB5]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK__Facturas__IDPedi__3B75D760] FOREIGN KEY([IDPedido])
REFERENCES [dbo].[Pedido] ([IDPedido])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK__Facturas__IDPedi__3B75D760]
GO
ALTER TABLE [dbo].[HistorialPedido]  WITH CHECK ADD  CONSTRAINT [FK__Historial__IDPed__3F466844] FOREIGN KEY([IDPedido])
REFERENCES [dbo].[Pedido] ([IDPedido])
GO
ALTER TABLE [dbo].[HistorialPedido] CHECK CONSTRAINT [FK__Historial__IDPed__3F466844]
GO
ALTER TABLE [dbo].[HistorialPedido]  WITH CHECK ADD  CONSTRAINT [FK__Historial__IDUsu__3E52440B] FOREIGN KEY([IDUsuario])
REFERENCES [dbo].[Usuario] ([IDUsuario])
GO
ALTER TABLE [dbo].[HistorialPedido] CHECK CONSTRAINT [FK__Historial__IDUsu__3E52440B]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK__Pedidos__EstadoI__34C8D9D1] FOREIGN KEY([EstadoID])
REFERENCES [dbo].[EstadoPedido] ([EstadoID])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK__Pedidos__EstadoI__34C8D9D1]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK__Pedidos__IDUsuar__33D4B598] FOREIGN KEY([IDUsuario])
REFERENCES [dbo].[Usuario] ([IDUsuario])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK__Pedidos__IDUsuar__33D4B598]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK__Productos__IDCat__2D27B809] FOREIGN KEY([IDCategoria])
REFERENCES [dbo].[Categoria] ([IDCategoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK__Productos__IDCat__2D27B809]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK__Usuarios__RoleID__286302EC] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RolUsuario] ([RoleID])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK__Usuarios__RoleID__286302EC]
GO
USE [master]
GO
ALTER DATABASE [Tienda] SET  READ_WRITE 
GO
