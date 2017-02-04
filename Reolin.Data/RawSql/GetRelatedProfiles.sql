
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[GetRelatedProfilesByTag]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].GetRelatedProfilesByTag
END

Go


Create Procedure   GetRelatedProfilesByTag(
   @ProfileId BigInt
)
as
Begin


Select	Top(10)	
		 p.*	
From [Profile]    as	p
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

						Where p1.Id   = @ProfileId
	   ))

   and p.Id	<>  @ProfileId
End