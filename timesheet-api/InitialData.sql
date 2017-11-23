insert into Users (Name, Password) values ('FoxMulder', '203B70B5AE883932161BBD0BDED9357E763E63AFCE98B16230BE33F0B94C2CC5')
insert into ProjectGroups(Name) values ('Customers')
insert into Projects(GroupId, Name) values (1, 'Customer A')
insert into Projects(GroupId, Name) values (1, 'Customer B')
insert into ProjectGroups(Name) values ('Projects')
insert into Projects(GroupId, Name) values (2, 'Project A')
insert into Projects(GroupId, Name) values (2, 'Project B')
insert into ProjectGroups(Name) values ('Features')
insert into Projects(GroupId, Name) values (3, 'Feature A')
insert into Projects(GroupId, Name) values (3, 'Feature B')
insert into Tasks(Name, Productive, ProjectGroupId) values ('Local Support', 0, null)
insert into Tasks(Name, Productive, ProjectGroupId) values ('Support', 1, 1)
insert into Tasks(Name, Productive, ProjectGroupId) values ('Bugfixing', 0, 2)
insert into Tasks(Name, Productive, ProjectGroupId) values ('Customization', 1, 1)
insert into Tasks(Name, Productive, ProjectGroupId) values ('Documentation', 0, 2)
insert into Tasks(Name, Productive, ProjectGroupId) values ('Development', 1, 2)
insert into Tasks(Name, Productive, ProjectGroupId) values ('Meeting', 0, null)