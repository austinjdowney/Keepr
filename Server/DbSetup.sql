CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
  creatorId VARCHAR (255) NOT NULL comment 'Vault Creator',
  name VARCHAR(255) NOT NULL comment 'Name of Vault',
  description VARCHAR(255) comment 'Description of Vault',
  img VARCHAR(255) comment 'Vault Picture',
  isPrivate BOOLEAN comment 'Vault private or public',
  createdAt DATETIME COMMENT 'Vault Created Time',
  updatedAt DATETIME COMMENT 'Vault Updated time',
) default charset utf8 comment '';