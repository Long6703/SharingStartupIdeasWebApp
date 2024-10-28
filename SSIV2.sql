USE [master]
IF EXISTS (SELECT [name] FROM master.dbo.sysdatabases WHERE name = N'SSIV2')
BEGIN
	ALTER DATABASE [SSIV2] SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE [SSIV2] SET ONLINE;
	DROP DATABASE [SSIV2];
END
create database SSIV2;

use SSIV2;

CREATE TABLE [user] (
    user_id INT PRIMARY KEY IDENTITY,
    displayname NVARCHAR(50) NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    role NVARCHAR(20) CHECK (role IN ('admin', 'startup', 'investor')) NOT NULL,
    status NVARCHAR(20) CHECK (status IN ('active', 'inactive')) DEFAULT 'active',
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE category (
    category_id INT PRIMARY KEY IDENTITY,
    name NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE idea (
    idea_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL,
    title NVARCHAR(255) NOT NULL,
    description TEXT NULL,
    category_id INT,
    created_at DATETIME DEFAULT GETDATE(),
    status NVARCHAR(20) CHECK (status IN ('approved', 'pending', 'rejected')) DEFAULT 'pending',
    is_seeking_investment BIT DEFAULT 0,
    is_implement BIT DEFAULT 0,
    poster_img NVARCHAR(255),
    
    FOREIGN KEY (user_id) REFERENCES [user](user_id),
    FOREIGN KEY (category_id) REFERENCES category(category_id)
);

CREATE TABLE ideadetail (
    idea_detail_id INT PRIMARY KEY IDENTITY,
    idea_id INT NOT NULL,
    content TEXT NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (idea_id) REFERENCES idea(idea_id)
);

CREATE TABLE image (
    image_id INT PRIMARY KEY IDENTITY,
    idea_detail_id INT NOT NULL,
    url NVARCHAR(255) NOT NULL,
    FOREIGN KEY (idea_detail_id) REFERENCES ideadetail(idea_detail_id)
);

CREATE TABLE comment (
    comment_id INT PRIMARY KEY IDENTITY,
    idea_detail_id INT NOT NULL,
    user_id INT NOT NULL,
    parent_id INT NULL,
    content TEXT NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (idea_detail_id) REFERENCES ideadetail(idea_detail_id),
    FOREIGN KEY (user_id) REFERENCES [user](user_id),
    FOREIGN KEY (parent_id) REFERENCES comment(comment_id)
);

CREATE TABLE investment_request (
    request_id INT PRIMARY KEY IDENTITY,
    idea_id INT NOT NULL,
    user_id INT NOT NULL,
    amount DECIMAL(18, 2) NOT NULL,
    status NVARCHAR(20) CHECK (status IN ('approved', 'rejected', 'pending')) DEFAULT 'pending',
    created_at DATETIME DEFAULT GETDATE(),
    equity_percentage DECIMAL(5, 2) CHECK (equity_percentage BETWEEN 0 AND 100) NOT NULL,
    investment_period NVARCHAR(50) NOT NULL,
    description TEXT NULL,
    FOREIGN KEY (idea_id) REFERENCES idea(idea_id),
    FOREIGN KEY (user_id) REFERENCES [user](user_id)
);

CREATE TABLE idea_interest (
    interest_id INT PRIMARY KEY IDENTITY,
    user_id INT NOT NULL,
    idea_id INT NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES [user](user_id),
    FOREIGN KEY (idea_id) REFERENCES idea(idea_id)
);

CREATE TABLE [transaction] (
    transaction_id INT PRIMARY KEY IDENTITY,
    investment_request_id INT NOT NULL,
    amount DECIMAL(18, 2) NOT NULL,
    status NVARCHAR(20) CHECK (status IN ('completed', 'pending', 'cancelled')) DEFAULT 'pending',
    created_at DATETIME DEFAULT GETDATE(),
    transaction_code NVARCHAR(50) UNIQUE,
    FOREIGN KEY (investment_request_id) REFERENCES investment_request(request_id)
);
INSERT INTO [user] (displayname, email, password, role, status)
VALUES 
    ('Admin User', 'admin@example.com', 'hashed_password1', 'admin', 'active'),
    ('Startup Founder', 'founder@example.com', 'hashed_password2', 'startup', 'active'),
    ('Investor John', 'investor1@example.com', 'hashed_password3', 'investor', 'active'),
    ('Inactive Startup', 'inactive_startup@example.com', 'hashed_password4', 'startup', 'inactive'),
    ('Investor Jane', 'investor2@example.com', 'hashed_password5', 'investor', 'active');

INSERT INTO category (name)
VALUES 
    ('Technology'),
    ('Health'),
    ('Education'),
    ('Environment'),
    ('Finance');
INSERT INTO idea (user_id, title, description, category_id, status, is_seeking_investment, is_implement, poster_img)
VALUES 
    (2, 'Smart Healthcare', 'A healthcare platform using AI for diagnostics', 1, 'approved', 1, 0, 'healthcare.jpg'),
    (2, 'E-learning App', 'A platform for online learning', 3, 'pending', 1, 0, 'elearning.jpg'),
    (3, 'Eco-friendly Packaging', 'Biodegradable packaging solutions', 4, 'rejected', 0, 1, 'eco.jpg'),
    (1, 'Fintech Solution', 'Blockchain-based finance management', 5, 'approved', 1, 1, 'fintech.jpg'),
    (4, 'Agritech', 'Technology solutions for agriculture', 1, 'pending', 0, 0, 'agritech.jpg');
INSERT INTO ideadetail (idea_id, content)
VALUES 
    (1, 'Detailed description of Smart Healthcare project'),
    (2, 'Detailed description of E-learning App'),
    (3, 'Details on Eco-friendly Packaging'),
    (4, 'Details on Fintech Solution'),
    (5, 'Details on Agritech project');
INSERT INTO image (idea_detail_id, url)
VALUES 
    (1, 'healthcare_img1.jpg'),
    (1, 'healthcare_img2.jpg'),
    (2, 'elearning_img1.jpg'),
    (4, 'fintech_img1.jpg'),
    (5, 'agritech_img1.jpg');
INSERT INTO comment (idea_detail_id, user_id, parent_id, content)
VALUES 
    (1, 3, NULL, 'Great healthcare idea!'),
    (2, 5, NULL, 'E-learning is the future'),
    (2, 5, 2, 'Agreed!'),
    (3, 2, NULL, 'Eco-friendly solutions are necessary'),
    (4, 1, NULL, 'Blockchain can be revolutionary');
INSERT INTO investment_request (idea_id, user_id, amount, status, equity_percentage, investment_period, description)
VALUES 
    (1, 3, 50000.00, 'pending', 10.00, '2 years', 'Investment for platform development'),
    (2, 3, 75000.00, 'approved', 15.00, '3 years', 'Investment for expansion'),
    (3, 5, 30000.00, 'rejected', 8.00, '1 year', 'Investment for eco-packaging research'),
    (4, 4, 120000.00, 'pending', 20.00, '5 years', 'Fintech app enhancement'),
    (5, 3, 45000.00, 'approved', 12.00, '2 years', 'Agritech project funding');
INSERT INTO idea_interest (user_id, idea_id)
VALUES 
    (3, 1),
    (5, 2),
    (3, 3),
    (4, 4),
    (2, 5);
INSERT INTO [transaction] (investment_request_id, amount, status, transaction_code)
VALUES 
    (1, 50000.00, 'completed', 'TXN001'),
    (2, 75000.00, 'pending', 'TXN002'),
    (3, 30000.00, 'cancelled', 'TXN003'),
    (4, 120000.00, 'completed', 'TXN004'),
    (5, 45000.00, 'pending', 'TXN005');
