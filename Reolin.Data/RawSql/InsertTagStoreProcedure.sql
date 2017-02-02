
Create	Procedure	InsertTag
(
	@ProfileId		BigInt,
	@TagName		nVarchar
)
as
Begin

declare	@newTagId	BigInt;

if not exists (Select [Name]	From Tag
					Where	Name	=	@TagName)
					Begin
						Insert	Tag(Name)
							Values(@TagName)

						Set	@newTagId	=	Scope_Identity();

					End
else
Begin
		Select	@newTagId	=	Id	
			from	Tag	
				Where	Name	=	@TagName
End

Insert	ProfileTag(ProfileId,	TagId)
			Values(@ProfileId,	@newTagId)
End