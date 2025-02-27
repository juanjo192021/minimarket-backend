-- CREATE DATABASE DB_MINIMARKET
-- USE DB_MINIMARKET;
-- SELECT @@SERVERNAME;


-- Table: Category (Main Categories)
CREATE TABLE Category (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    CategoryImageUrl VARCHAR(500) DEFAULT NULL,
    ParentCategoryId INT DEFAULT NULL,
	CategoryLevel INT DEFAULT 1 NOT NULL,
    CreationDate DATETIME2 DEFAULT GETDATE() NOT NULL,
    LastUpdateDate DATETIME2 DEFAULT NULL,
    CONSTRAINT FK_Category_Parent FOREIGN KEY (ParentCategoryId) REFERENCES Category(Id) ON DELETE NO ACTION
);

-- Table: Brand
CREATE TABLE Brand (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    BrandImageUrl VARCHAR(500) DEFAULT NULL,
    Status BIT DEFAULT 1 NOT NULL,
    CreationDate DATETIME2 DEFAULT GETDATE() NOT NULL,
    LastUpdateDate DATETIME2 DEFAULT NULL
);


-- Table: UserType (User categories)
CREATE TABLE UserType (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL UNIQUE -- Admin, Customer, Employee, etc.
);

-- Table: SystemUser 
CREATE TABLE SystemUser (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(500) NOT NULL,
    FirstName VARCHAR(60) NOT NULL,
    LastName VARCHAR(60) NOT NULL,
    ProfileImageUrl VARCHAR(500) DEFAULT NULL,
    DocumentType VARCHAR(20) NOT NULL,
    DocumentNumber VARCHAR(20) NOT NULL UNIQUE,
    BirthDate DATE NOT NULL,
    Phone VARCHAR(20) NULL,
    Mobile VARCHAR(20) NOT NULL,
    Gender VARCHAR(20) NULL,
    Address VARCHAR(200) NOT NULL,
    Confirmed BIT NOT NULL DEFAULT 0,
    Status BIT NOT NULL DEFAULT 1,
    RegistrationDate DATETIME2 DEFAULT GETDATE() NOT NULL,
    UserTypeId INT NOT NULL,
    CONSTRAINT FK_SystemUser_UserType FOREIGN KEY (UserTypeId) REFERENCES UserType(Id)
);


-- Table: Role
CREATE TABLE Role (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL UNIQUE
);

-- Table: UserRole
CREATE TABLE UserRole (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SystemUserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (SystemUserId) REFERENCES SystemUser (Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES Role(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_SystemUser_Role UNIQUE (SystemUserId, RoleId)
);
-- Table: Product
CREATE TABLE Product (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(200) NOT NULL,
    Description VARCHAR(500),
    ProductImageUrl VARCHAR(500) DEFAULT NULL,
    Status BIT DEFAULT 1 NOT NULL,
    CreationDate DATETIME2 DEFAULT GETDATE() NOT NULL,
    LastUpdateDate DATETIME2 DEFAULT GETDATE() NULL,
    CreatedBy INT NOT NULL,
    LastUpdatedBy INT NULL,
    BrandId INT NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT FK_Product_Brand FOREIGN KEY (BrandId) REFERENCES Brand(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryId) REFERENCES Category(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Product_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES SystemUser(Id) ON DELETE NO ACTION,
	CONSTRAINT FK_Product_LastUpdatedBy FOREIGN KEY (LastUpdatedBy) REFERENCES SystemUser(Id) ON DELETE NO ACTION
);

-- Table: ProductPresentation
CREATE TABLE ProductPresentation (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    Unit VARCHAR(20) NOT NULL
);

-- Table: ProductFlavor
CREATE TABLE ProductFlavor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL
);

-- Table: ProductVariant
CREATE TABLE ProductVariant (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL,
    PresentationId INT NULL,
    FlavorId INT NULL,
    Sku VARCHAR(50) NOT NULL UNIQUE,
    BarCode VARCHAR(60) NOT NULL UNIQUE,
    Description VARCHAR(500),
    ProductVariantImageUrl VARCHAR(500) DEFAULT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
	CreationDate DATETIME2 DEFAULT GETDATE() NOT NULL,
    LastUpdateDate DATETIME2 DEFAULT GETDATE() NULL,
    CreatedBy INT NOT NULL,
    LastUpdatedBy INT NULL,
    CONSTRAINT FK_ProductVariant_Product FOREIGN KEY (ProductId) REFERENCES Product(Id) ON DELETE CASCADE,
	CONSTRAINT FK_ProductVariant_Presentation FOREIGN KEY (PresentationId) REFERENCES ProductPresentation(Id) ON DELETE SET NULL,
	CONSTRAINT FK_ProductVariant_Flavor FOREIGN KEY (FlavorId) REFERENCES ProductFlavor(Id) ON DELETE SET NULL,
	CONSTRAINT FK_ProductVariant_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES SystemUser(Id) ON DELETE CASCADE,
	CONSTRAINT FK_ProductVariant_LastUpdatedBy FOREIGN KEY (LastUpdatedBy) REFERENCES SystemUser(Id) ON DELETE NO ACTION,
	CONSTRAINT CK_ProductVariant_Stock CHECK (Stock >= 0),
	CONSTRAINT CK_ProductVariant_Price CHECK (Price >= 0)

);

-- Table: Region
CREATE TABLE Region (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    RegionCode VARCHAR(50) UNIQUE NOT NULL, 
    Name VARCHAR(50) NOT NULL
);

-- Table: Province
CREATE TABLE Province (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    RegionId INT NOT NULL,
    CONSTRAINT FK_Province_Region FOREIGN KEY (RegionId) REFERENCES Region(Id) ON DELETE CASCADE
);

-- Table: District
CREATE TABLE District (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    ProvinceId INT NOT NULL,
    CONSTRAINT FK_District_Province FOREIGN KEY (ProvinceId) REFERENCES Province(Id) ON DELETE CASCADE
);


-- Table: Sale
CREATE TABLE Sale (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SystemUserId INT NOT NULL,
    TransactionId VARCHAR(100) NOT NULL UNIQUE,
    InvoiceType VARCHAR(20) NOT NULL,
    InvoiceNumber VARCHAR(50) NOT NULL UNIQUE,
    DistrictId INT NOT NULL,
    Address VARCHAR(200) NOT NULL,
    SaleDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    Contact VARCHAR(150) NOT NULL,
    Phone VARCHAR(20) NULL,
    Mobile VARCHAR(20) NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
	Status VARCHAR(20) NOT NULL DEFAULT 'Pending',
	LastUpdateDate DATETIME2 DEFAULT GETDATE() NULL,
	CreatedBy INT NULL,
	LastUpdatedBy INT NULL,
	CONSTRAINT FK_Sale_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES SystemUser(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Sale_LastUpdatedBy FOREIGN KEY (LastUpdatedBy) REFERENCES SystemUser(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Sale_SystemUser FOREIGN KEY (SystemUserId) REFERENCES SystemUser (Id),
    CONSTRAINT FK_Sale_District FOREIGN KEY (DistrictId) REFERENCES District(Id),
	CONSTRAINT CK_Sale_Status CHECK (Status IN ('Pending', 'Completed', 'Cancelled')),
	CONSTRAINT UQ_Sale_TransactionId UNIQUE (TransactionId)
);

-- Table: SaleDetail
CREATE TABLE SaleDetail (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Quantity INT NOT NULL,
    HistoricalUnitPrice DECIMAL(10,2) NOT NULL,
	TotalPrice AS (Quantity * HistoricalUnitPrice) PERSISTED,
    SaleId INT NOT NULL,
    ProductVariantId INT NOT NULL,
    CONSTRAINT FK_SaleDetail_Sale FOREIGN KEY (SaleId) REFERENCES Sale(Id),
    CONSTRAINT FK_SaleDetail_ProductVariant FOREIGN KEY (ProductVariantId) REFERENCES ProductVariant(Id),
	CONSTRAINT CK_SaleDetail_Quantity CHECK (Quantity > 0),
	CONSTRAINT UQ_SaleDetail UNIQUE (SaleId, ProductVariantId)
);


-- Table: PaymentMethod (Opcional para normalizar métodos de pago)
CREATE TABLE PaymentMethod (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL UNIQUE, -- Ejemplo: Visa, Mastercard, Yape, Plin
    PaymentMethodImageUrl VARCHAR(500) DEFAULT NULL,
);

-- Table: PaymentHistory
CREATE TABLE PaymentHistory (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SaleId INT NOT NULL,
    PaymentDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    PaymentMethodId INT NOT NULL, -- FK a PaymentMethod
    Amount DECIMAL(10,2) NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Pending', -- Pending, Completed, Failed
    CONSTRAINT FK_PaymentHistory_Sale FOREIGN KEY (SaleId) REFERENCES Sale(Id) ON DELETE CASCADE,
    CONSTRAINT FK_PaymentHistory_Method FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethod(Id),
	CONSTRAINT CK_PaymentHistory_Status CHECK (Status IN ('Pending', 'Completed', 'Failed')),
	CONSTRAINT UQ_PaymentHistory_Sale_Method UNIQUE (SaleId, PaymentMethodId)
);

CREATE INDEX IDX_Category_Parent ON Category(ParentCategoryId);
CREATE INDEX IDX_Category_Name ON Category(Name);
CREATE INDEX IDX_Brand_Name ON Brand(Name);
CREATE INDEX IDX_SystemUser_Email ON SystemUser(Email);
CREATE INDEX IDX_SystemUser_UserType ON SystemUser(UserTypeId);
CREATE INDEX IDX_Product_Name ON Product(Name);
CREATE INDEX IDX_Product_Category ON Product(CategoryId);
CREATE INDEX IDX_Product_Brand ON Product(BrandId);
CREATE INDEX IDX_ProductVariant_Product ON ProductVariant(ProductId);
CREATE INDEX IDX_ProductVariant_Sku ON ProductVariant(Sku);
CREATE INDEX IDX_ProductVariant_BarCode ON ProductVariant(BarCode);
CREATE INDEX IDX_Sale_User ON Sale(SystemUserId);
CREATE INDEX IDX_Sale_Date ON Sale(SaleDate);
CREATE INDEX IDX_SaleDetail_Sale ON SaleDetail(SaleId);
CREATE INDEX IDX_SaleDetail_ProductVariant ON SaleDetail(ProductVariantId);
CREATE INDEX IDX_Sale_User_Date ON Sale(SystemUserId, SaleDate);
CREATE INDEX IDX_SaleDetail_Sale_ProductVariant ON SaleDetail(SaleId, ProductVariantId);
CREATE INDEX IDX_PaymentHistory_Sale ON PaymentHistory(SaleId);