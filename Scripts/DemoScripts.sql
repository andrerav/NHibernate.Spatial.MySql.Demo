SELECT * FROM mysqldemodb.municipality;
SELECT * FROM mysqldemodb.county;

select * from mysqldemodb.county c
	join mysqldemodb.municipality m on st_within(m.Area, st_buffer(c.area, 0.0001))
	where c.id = 18