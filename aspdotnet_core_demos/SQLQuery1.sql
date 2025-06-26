create procedure sp_insert(@ec int,@en varchar(20),@sal int,@did int)
as
	insert into employees (ecode,ename,salary,deptid)
		values(@ec,@en,@sal,@did)

go
create procedure sp_delete(@ec int)
as
	delete from employees where ecode=@ec

go
create procedure sp_update(@ec int,@en varchar(20),@sal int,@did int)
as
	update employees set ename=@en,salary=@sal,deptid=@did		
			where ecode=@ec

go
create procedure sp_getemps
as
	select * from employees

go
create procedure sp_getbyid(@ec int)
as
	select * from employees where ecode=@ec


go --for Identity insert value
alter procedure sp_place_order(@amount int,@qty int,@order_id int out)
as
insert into orders values(@amount,@qty)
select @order_id=SCOPE_IDENTITY()
