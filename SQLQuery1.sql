USE QLSINHVIENSS 
GO
SELECT * FROM dbo.khoas

SET IDENTITY_INSERT dbo.khoas ON
INSERT INTO dbo.khoas (Id,TenKhoa)VALUES(1,N'FullStack')
SET IDENTITY_INSERT dbo.khoas OFF

SELECT * FROM dbo.lops

SET IDENTITY_INSERT dbo.lops ON
INSERT INTO dbo.lops(Id, TenLop, SiSo, MaKhoa) VALUES (1, N'a', 30, 1)
SET IDENTITY_INSERT dbo.lops OFF

