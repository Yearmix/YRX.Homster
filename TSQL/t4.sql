/* Чтоб ты ссука всю жизнь на коленке писал, и писял криво !!! */

SELECT d.Id, d.name, max(t.a) max_day, max(t.b) from (SELECT ss.[Employee_Id], (SELECT top 1 
    a = DATEDIFF ( day ,lag(s.date) over (ORDER by s.date), s.date) 
	FROM Salary s 
	where s.[Employee_Id] = ss.[Employee_Id]
	order by DATEDIFF ( day ,lag(s.date) over (ORDER by s.date), s.date) desc) a, max (ss.amount) b
FROM Salary ss
group by ss.[Employee_Id]) t
inner join Employee e on e.id = t.Employee_Id
inner join Department d on d.Id = e.Department_Id
group by d.Id, d.name
