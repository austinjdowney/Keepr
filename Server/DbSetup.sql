CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary vault key',
  creatorId VARCHAR (255) NOT NULL comment 'FK: Vault Creator',
  name VARCHAR(255) NOT NULL comment 'Name of Vault',
  description VARCHAR(255) comment 'Description of Vault',
  img VARCHAR(255) comment 'Vault Picture',
  isPrivate BOOLEAN comment 'Vault private or public',
  createdAt DATETIME COMMENT CURRENT_TIMESTAMP 'Vault Created Time',
  updatedAt DATETIME COMMENT DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP 'Vault Updated time',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 comment '';
CREATE TABLE IF NOT EXISTS keeps(
  id int NOT NULL primary key AUTO_INCREMENT comment 'primary keep key',
  creatorId VARCHAR (255) NOT NULL comment 'FK: Keep Creator',
  name VARCHAR(255) NOT NULL comment 'Name of Keep',
  description VARCHAR(255) comment 'Description of Keep',
  img VARCHAR(255) comment 'keep Picture',
  views INT comment 'Number of views of keep',
  shares INT comment 'Number of shares',
  keeps INT comment 'Total number of times kept',
  createdAt DATETIME COMMENT CURRENT_TIMESTAMP 'Vault Created Time',
  updatedAt DATETIME COMMENT DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP 'Vault Updated time',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 comment '';
CREATE TABLE IF NOT EXISTS vaultkeep(
  id INT NOT NULL comment 'id for the keeps of a vault',
  creatorId VARCHAR (255) NOT NULL comment 'FK: vaultkeep Creator',
  vaultId INT NOT NULL comment 'FK ID of vault for user',
  keepId INT NOT NULL comment 'FK ID of keep for user',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 comment '';