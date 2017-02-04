
declare	@tagName	nVarchar(100)	=	N'C++';

Select	Top(10)	
				p.*,
				a.Location.Lat	as	Latitude,
				a.Location.Long	as	Longitude

					From [Profile]    as	p
					Inner	Join	Address	a
					On		a.Id	=	p.Id
					Where	a.Location.STDistance('' -- do some thing with it
					Inner   Join ProfileTag
						On p.id            = ProfileTag.ProfileId
					Inner   Join Tag	as	t
						On ProfileTag.TagId = t.Id
					
					Where t.Id in 
					  (Select Tag.Id
						   From Tag Where   Tag.Id  in
						   (
								Select t1.Id   as	tagId
										from    Tag as	t1
											Inner   Join ProfileTag
											on t1.Id		= ProfileTag.TagId
											Inner Join    Profile as	p1
											on  p1.Id		= ProfileTag.ProfileId
											where	t.Name	=	@tagName
						
						   )
						)
				