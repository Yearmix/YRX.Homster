SELECT u.Id, u.Name
  STUFF(
         (SELECT ', ' + r.RoleName
          FROM roles r
          WHERE r.User_Id = u.Id
          FOR XML PATH (''))
          , 1, 1, '')  AS roles
FROM users u;