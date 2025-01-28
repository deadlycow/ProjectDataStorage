insert into Employee (name) values ('Kungen'),('Andreas'),('Lärling')
insert into StatusType (name) values ('Ej påbörjad'),('Pågående'),('Avslutad')
insert into ServiceType (name, Price) values ('Service 1', 100),('Service 2', 200),('Service 3', 300)
insert into Customer (name) values ('Kund 1'),('Kund 2'),('Kund 3')
insert into Project (Name, StartDate, EndDate, CustomerId, EmployeeId, StatusTypeId) 
 values 
   ('Projekt 1', '2019-01-01', '2019-01-02', 1, 1, 1), ('Projekt 2', '2019-01-01', '2019-01-02', 2, 2, 2),('Projekt 3', '2019-01-01', '2019-01-02', 3, 3, 3),('Projekt 4', '2019-01-01', '2019-01-02', 1, 1, 1),('Projekt 5', '2019-01-01', '2019-01-02', 2, 2, 2),('Projekt 6', '2019-01-01', '2019-01-02', 3, 3, 3),('Projekt 7', '2019-01-01', '2019-01-02', 1, 1, 1),('Projekt 8', '2019-01-01', '2019-01-02', 2, 2, 2),('Projekt 9', '2019-01-01', '2019-01-02', 3, 3, 3),('Projekt 10', '2019-01-01', '2019-01-02', 1, 1, 1),('Projekt 11', '2019-01-01', '2019-01-02', 2, 2, 2),('Projekt 12', '2019-01-01', '2019-01-02', 3, 3, 3),('Projekt 13', '2019-01-01', '2019-01-02', 1, 1, 1),('Projekt 14', '2019-01-01', '2019-01-02', 2, 2, 2),('Projekt 15', '2019-01-01', '2019-01-02', 3, 3, 3),('Projekt 16', '2019-01-01', '2019-01-02', 1, 1, 1),('Projekt 17', '2019-01-01', '2019-01-02', 2, 2, 2),('Projekt 18', '2019-01-01', '2019-01-02', 3, 3, 3),('Projekt 19', '2019-01-01', '2019-01-02', 1, 1, 1)
insert into ProjectService (ProjectId, ServiceId) values (100, 1),(100, 2),(100, 3),(101, 1),(101, 2),(101, 3),(102, 1),(102, 2),(102, 3),(103, 1),(103, 2),(103, 3),(104, 1),(104, 2),(104, 3),(105, 1),(105, 2),(105, 3),(106, 1),(106, 2),(106, 3),(107, 1),(107, 2),(107, 3),(108, 1),(108, 2),(108, 3),(109, 1),(109, 2),(109, 3),(110, 1),(110, 2),(110, 3),(111, 1),(111, 2),(111, 3),(112, 1),(112, 2),(112, 3),(113, 1),(113, 2),(113, 3),(114, 1),(114, 2),(114, 3),(115, 1),(115, 2),(115, 3),(116, 1),(116, 2),(116, 3),(117, 1),(117, 2),(117, 3),(118, 1),(118, 2),(118, 3)


--select p.ProjectNumber, p.Name Project_Name ,p.StartDate, c.Name Customer, e.Name Employee, stt.Name Status, st.Name Service, st.Price
--from Project as p
--join Customer as c on p.CustomerId = c.Id
--join Employee as e on p.EmployeeId = e.Id
--join ProjectService as ps on p.Id = ps.ProjectId
--join ServiceType as st on ps.ServiceId = st.Id
--join StatusType as stt on p.StatusTypeId = stt.Id
--where CustomerId = 3


