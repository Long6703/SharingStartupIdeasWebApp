create database SSIV2;

use SSIV2;

CREATE TABLE [user] (
    user_id INT PRIMARY KEY IDENTITY,
    displayname NVARCHAR(50) NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    role NVARCHAR(20),
	avatar_url VARCHAR(100) NULL,
	bio NVARCHAR(500) NULL,
    location NVARCHAR(100) NULL,
    profession NVARCHAR(100) NULL,
    website_url NVARCHAR(255) NULL,
    linkedin_url NVARCHAR(255) NULL,
    twitter_url NVARCHAR(255) NULL,
    facebook_url NVARCHAR(255) NULL,
    created_at DATETIME DEFAULT GETDATE(),
	status NVARCHAR(20),
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
    status NVARCHAR(20),
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
    status NVARCHAR(20),
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
    status NVARCHAR(20),
    created_at DATETIME DEFAULT GETDATE(),
    transaction_code NVARCHAR(50) UNIQUE,
    FOREIGN KEY (investment_request_id) REFERENCES investment_request(request_id)
);
