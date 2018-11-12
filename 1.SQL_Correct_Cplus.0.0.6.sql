-- requpete 1
Select * from YearStudies

Update YearStudies set Code = 1, Reference = 1 where [Name] = '1ère année';
Update YearStudies set Code = 2, Reference = 2 where [Name] = '2ème année';
Update YearStudies set Code = 3, Reference = 3 where [Name] = '3ème année';



Delete  from YearStudies where Id > 3

Select * from YearStudies