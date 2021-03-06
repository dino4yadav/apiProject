USE [demo]
GO
/****** Object:  Table [dbo].[MedicineStorageDetails]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineStorageDetails](
	[MedicineID] [int] IDENTITY(1,1) NOT NULL,
	[MedicineName] [nvarchar](100) NOT NULL,
	[ManufacturedBy] [nvarchar](100) NOT NULL,
	[ManufactureDate] [datetime] NOT NULL,
	[ExpiryDate] [datetime] NOT NULL,
	[SuppliersID] [int] NOT NULL,
	[RetailPrice] [money] NULL,
	[Discount] [decimal](18, 0) NULL,
	[TypeOfMedicine] [nvarchar](100) NULL,
	[StorageCondition] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedSupplier]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedSupplier](
	[MedSupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](150) NOT NULL,
	[ContactNumberFirst] [nvarchar](10) NOT NULL,
	[ContactNumberTwo] [nvarchar](10) NULL,
	[SupplierAddress] [nvarchar](200) NULL,
	[SupplierLicence] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MedSupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetails]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetails](
	[PurchaseID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[MedicineName] [nvarchar](100) NULL,
	[Medicinetype] [nvarchar](100) NULL,
	[ExpiryDate] [nvarchar](100) NULL,
	[PurchasePrice] [money] NULL,
	[TaxOnMedicine] [money] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateBy] [nvarchar](100) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[registration]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[registration](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[PhoneNumber] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCartData]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCartData](
	[MedicineID] [int] NULL,
	[UserName] [nvarchar](100) NULL,
	[Quantity] [int] NULL,
	[CartCreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[cartBilling]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[cartBilling]  (
@UserName nvarchar(100)
)
as begin
select  U.MedicineID,M.MedicineName,U.Quantity,
M.RetailPrice, M.Discount,M.ExpiryDate,M.StorageCondition,
(U.Quantity * M.RetailPrice) as MedicinePrice,
((M.RetailPrice * M.Discount) / 100) * U.Quantity  as discountamout,
((U.Quantity * M.RetailPrice)) -
(((M.RetailPrice * M.Discount) / 100)* U.Quantity) as finalprice
from 
UserCartData as U 
inner join MedicineStorageDetails as M 
on M.MedicineID = U.MedicineID Where U.UserName = @UserName
group by M.MedicineName,M.RetailPrice, M.Discount,
M.ExpiryDate,M.StorageCondition,U.Quantity,U.MedicineID


end
GO
/****** Object:  StoredProcedure [dbo].[checkUserAndPassword]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[checkUserAndPassword] (
@userName nvarchar(100),
@password nvarchar(100) 
)
As begin 
select * from registration Where 
UserName = @userName and Password = @password
end
GO
/****** Object:  StoredProcedure [dbo].[getCartMedecine]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getCartMedecine] 
( 
@serchText nvarchar(10)
)
as begin
declare @qry nvarchar(Max)
set @qry = 'select * from MedicineStorageDetails
Where MedicineName Like '+@serchText +''
Exec(@qry)
end
GO
/****** Object:  StoredProcedure [dbo].[getProfileData]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getProfileData] ( 
@UserName nvarchar(100)
)
as begin 
select * from registration 
Where UserName = @UserName
End
GO
/****** Object:  StoredProcedure [dbo].[RemoveCartData]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[RemoveCartData] (
@UserName nvarchar(100),
@MedicineID int
) 
as begin 
delete from UserCartData 
Where MedicineID = @MedicineID 
and UserName = @UserName

exec cartBilling @UserName

End
GO
/****** Object:  StoredProcedure [dbo].[SaveUserCartData]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SaveUserCartData] (   
@MedicineID int,   
@UserName nvarchar(100),  
@Quantity int  
)  
as begin   
if @MedicineID = ( select top 1 MedicineID from UserCartData Where MedicineID
= @MedicineID and UserName = @UserName )
begin 
update UserCartData set Quantity = 
(convert(int,( select top 1 Quantity from UserCartData Where MedicineID
= @MedicineID and UserName = @UserName )) +  convert(int,@Quantity))
Where MedicineID = @MedicineID and UserName = @UserName 
End 
else begin 
insert into UserCartData  
select @MedicineID,@UserName,@Quantity,getdate(),  
null,'false' 
End
select sum(Quantity) as CountValue from UserCartData 
where UserName = @UserName
end
GO
/****** Object:  StoredProcedure [dbo].[saveUserData]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[saveUserData] (

@Username nvarchar(100),
@Password nvarchar(100),
@EmailAddress nvarchar(100),
@PhoneNumber int

)
as begin
insert into registration 
( UserName, Password, EmailAddress , PhoneNumber ) 
values
( @Username, @Password,@EmailAddress,@PhoneNumber )
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateCartData]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[UpdateCartData] (
@UserName nvarchar(100),
@MedicineID int, 
@Quantity int 
) 
as begin 
update UserCartData 
set Quantity = @Quantity 
Where MedicineID = @MedicineID 
and UserName = @UserName

exec cartBilling @UserName

End
GO
/****** Object:  StoredProcedure [dbo].[UpdateProfileData]    Script Date: 10/6/2021 3:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateProfileData] (  
@UserID int,
@UserName nvarchar(100),
@Password nvarchar(100),
@Email nvarchar(100),
@phone int
)  
as begin   
update registration set   
UserName = @UserName , Password = @Password,  
EmailAddress = @Email , PhoneNumber = @phone
Where UserID = @UserID
  
select * from registration Where UserID = @UserID 
End  
GO
