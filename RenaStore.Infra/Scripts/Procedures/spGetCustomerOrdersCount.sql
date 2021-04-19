CREATE PROCEDURE spGetCustomerOrdersCount
    @Document CHAR(11)
AS

select 
    c.Id,
    concat(c.FirstName, ' ', c.LastName) as Name,
    c.Document,
    (
        select 
            count(o.Id) 
        from [dbo].[Order] o 
            where o.CustomerId = c.Id
    ) as Orders
from
    Customer c
where c.Document = @Document