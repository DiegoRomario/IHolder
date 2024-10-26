 INSERT INTO Product (Id, CategoryId, Name, Description, Risk, ExchangeId, CreatedAt) VALUES
('ABCDABCD-ABCD-ABCD-ABCD-ABCDABCDABCD', '12345678-1234-1234-1234-123456789ABC', 'Brazilian stocks', 'Stocks of companies traded on Brazilian stock exchanges.', 5, 'SA', GETDATE()),
('ABCDEF12-3456-7890-ABCD-EF1234567890', '12345678-1234-1234-1234-123456789ABC', 'Brazilian real estate funds (FIIS)', 'Investment funds focused on real estate assets in Brazil.', 4, 'SA', GETDATE()),
('ABCDEFAB-CDEF-ABCD-EFAB-CDEFABCDEFAB', '12345678-1234-1234-1234-123456789ABC', 'Stocks', 'Shares of ownership in companies, offering potential returns but with high risk.', 5, NULL, GETDATE()),
('DEADBEEF-CAFE-BABE-FACE-FEEDBEADFACE', '12345678-1234-1234-1234-123456789ABC', 'Real Estate Investment Trust (REITS)', 'Real Estate Investment Trusts that generate income through property-related investments.', 4, NULL, GETDATE());
