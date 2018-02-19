select u.Id, u.Name
  STUFF(
         (SELECT ', ' + r.RoleName
          FROM roles r
          where r.User_Id = u.Id
          FOR XML PATH (''))
          , 1, 1, '')  AS roles
from users u;