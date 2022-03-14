

/****** Object:  Table [dbo].[Products]    Script Date: 15-03-2022 02:14:44 ******/

IF EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
DROP TABLE Products

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
	[Size] [char](10) NULL,
	[Price] [int] NULL,
	[Colour] [varchar](50) NULL,
	[Made] [varchar](50) NULL,
	[Style] [varchar](50) NULL,
	[Gender] [int] NULL,
	[Image] [varchar](500) NULL,
	[LastTime] [datetime] NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]
GO



GO

/****** Object:  StoredProcedure [dbo].[Proc_AddUpdateProduct]    Script Date: 15-03-2022 02:12:21 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Proc_AddUpdateProduct')
DROP PROCEDURE [dbo].[Proc_AddUpdateProduct]
GO

/****** Object:  StoredProcedure [dbo].[Proc_AddUpdateProduct]    Script Date: 15-03-2022 02:12:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
CREATE PROCEDURE [dbo].[Proc_AddUpdateProduct]
	-- Add the parameters for the stored procedure here
	@Id int,
	@Description varchar(500),
	@Size varchar(20),
	@Price int,
	@Colour varchar(50),
	@Made varchar(50),
	@Style varchar(50),
	@Gender varchar(10),
	@Image varchar(500)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @OP int
	If(@Id=0)
	begin
	Insert Into Products([Description], [Size], [Price], [Colour], [Made], [Style], [Gender], [Image], [LastTime], [IsActive])
	values(@Description,@Size,@Price,@Colour,@Made,@Style,@Gender,@Image,getutcdate(),1)
	 SET @OP = @@IDENTITY;
	end

	If(@Id>0)
	begin
	Update Products
	set Description=@Description,Size=@Size,Price=@Price,Colour=@Colour,Made=@Made,Gender=@Gender,Style=@Style,
	[Image]=case @Image when '' then Image else @Image end,LastTime=getutcdate()
	where id=@Id

	SET @OP=0
	end

	select  @OP
END
GO



GO

/****** Object:  StoredProcedure [dbo].[Proc_DeleteProd]    Script Date: 15-03-2022 02:13:20 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Proc_DeleteProd')
DROP PROCEDURE [dbo].[Proc_DeleteProd]
GO

/****** Object:  StoredProcedure [dbo].[Proc_DeleteProd]    Script Date: 15-03-2022 02:13:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_DeleteProd]
	-- Add the parameters for the stored procedure here
	@Id int


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
update  Products set IsActive=0 where Id=@Id


END
GO



GO

/****** Object:  StoredProcedure [dbo].[Proc_GetAllorSingleProduct]    Script Date: 15-03-2022 02:13:34 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Proc_GetAllorSingleProduct')
DROP PROCEDURE [dbo].[Proc_GetAllorSingleProduct]
GO

/****** Object:  StoredProcedure [dbo].[Proc_GetAllorSingleProduct]    Script Date: 15-03-2022 02:13:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetAllorSingleProduct]
	-- Add the parameters for the stored procedure here
@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 select [Id], [Description], [Size], [Price], [Colour], [Made], [Style], [Gender], [Image], [LastTime], [IsActive] from Products where (Id=@Id and @id<>0) OR (@Id=0) and IsActive=1
END
GO


