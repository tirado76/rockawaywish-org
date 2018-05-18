select		tbUsers.UserId
			, tbUsers.FirstName
			, tbUsers.LastName
			, tbUsers. Email
			, tbUsers.DateCreated
			, tbUsers.IsDonator
			, tbUsers.IsUser
			, tbPaymentTypes.Name [PaymentType]
			, tbUserPayments.PaymentMethod
			, tbUserPayments.PaymentAmount
from		dbo.Users tbUsers
join		dbo.UserPayments tbUserPayments on tbUserPayments.UserId = tbUsers.UserId
join		dbo.PaymentTypes tbPaymentTypes on tbPaymentTypes.PaymentTypeId = tbUserPayments.PaymentTypeId
order by	tbUsers.DateCreated desc