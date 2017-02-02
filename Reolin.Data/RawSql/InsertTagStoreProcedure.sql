Go
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[InsertTag]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].InsertTag
END

Go

Create	Procedure	InsertTag
(
	@ProfileId		BigInt	=	-1,
	@AddressId		BigInt	=	-1,
	@TagName		nVarchar(60)
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

if	Exists(	Select	Name	
			from	Tag
				Inner	Join	ProfileTag
				On
				Tag.Id	=	ProfileTag.TagId
				Where	
						Tag.Id		=	@newTagId
							and	
						ProfileId	=	@ProfileId)
						return @newTagId;

if(@AddressId	= -1)
	Insert	ProfileTag(ProfileId,	TagId)
			Values(@ProfileId,	@newTagId);
else
	Insert	AddressTag(AddressId,	TagId)
				Values(@AddressId,	@newTagId)
End