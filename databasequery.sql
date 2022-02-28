--SEAL TRAVELS WEBSYSTEM

--Administrator
create table ADMINISTRATOR
(
AdministratorId nvarchar(50),
Password nvarchar(50),
)
insert into ADMINISTRATOR(AdministratorId,Password)
values('admin01','admin01');

--Users
create table USERS
(
Id int Primary Key Identity(1,1),
Email nvarchar(50),
Username nvarchar(50),
Password nvarchar(50),
DateAdded nvarchar(50)
)

create procedure spUSERS
@Email nvarchar(50),
@Username nvarchar(50),
@Password nvarchar(50),
@DateAdded nvarchar(50)
as
begin
insert into USERS(Email,Username,Password,DateAdded)
values(@Email,@Username,@Password,@DateAdded)
end

create procedure spUpdateUSERS
@Id int,
@Email nvarchar(50),
@Username nvarchar(50),
@DateAdded nvarchar(50)
as
begin
update USERS set Email=@Email,Username=@Username,DateAdded=@DateAdded
where Id=@Id
end

create procedure spDeleteUSERS
@Id int
as
begin
delete from USERS
where Id=@Id
end


--Flight Booking - Roundtrip
create table ROUNDTRIPFLIGHTS
(
Id int Primary Key Identity(1,1),
Name nvarchar(50),
Email nvarchar(50),
Contact nvarchar(50),
TripType nvarchar(50),
LeavingFrom nvarchar(50),
GoingTo nvarchar(50),
Departing nvarchar(50),
Returning nvarchar(50),
Adults nvarchar(50),
Children nvarchar(50),
FlightType nvarchar(50),
Airline nvarchar(50),
TravelClass nvarchar(50),
Amount nvarchar(50)
)

create procedure spROUNDTRIPFLIGHTS
@Name nvarchar(50),
@Email nvarchar(50),
@Contact nvarchar(50),
@TripType nvarchar(50),
@LeavingFrom nvarchar(50),
@GoingTo nvarchar(50),
@Departing nvarchar(50),
@Returning nvarchar(50),
@Adults nvarchar(50),
@Children nvarchar(50),
@FlightType nvarchar(50),
@Airline nvarchar(50),
@TravelClass nvarchar(50),
@Amount nvarchar(50)
as
begin
insert into ROUNDTRIPFLIGHTS(Name,Email,Contact,TripType,LeavingFrom,GoingTo,Departing,Returning,Adults,Children,FlightType,Airline,TravelClass,Amount)
values(@Name,@Email,@Contact,@TripType,@LeavingFrom,@GoingTo,@Departing,@Returning,@Adults,@Children,@FlightType,@Airline,@TravelClass,@Amount)
end

create procedure spUpdateROUNDTRIPFLIGHTS
@Id int,
@Name nvarchar(50),
@Email nvarchar(50),
@Contact nvarchar(50),
@TripType nvarchar(50),
@LeavingFrom nvarchar(50),
@GoingTo nvarchar(50),
@Departing nvarchar(50),
@Returning nvarchar(50),
@Adults nvarchar(50),
@Children nvarchar(50),
@FlightType nvarchar(50),
@Airline nvarchar(50),
@TravelClass nvarchar(50),
@Amount nvarchar(50)
as
begin
update ROUNDTRIPFLIGHTS set Name=@Name,Email=@Email,Contact=@Contact,TripType=@TripType,LeavingFrom=@LeavingFrom,GoingTo=@GoingTo,Departing=@Departing,Returning=@Returning,Adults=@Adults,Children=@Children,FlightType=@FlightType,Airline=@Airline,TravelClass= @TravelClass,Amount=@Amount
where Id=@Id
end

create procedure spDeleteROUNDTRIPFLIGHTS
@Id int
as
begin
delete from ROUNDTRIPFLIGHTS
where Id=@Id
end

--Flight Booking - Oneway
create table ONEWAYFLIGHTS
(
Id int Primary Key Identity(1,1),
Name nvarchar(50),
Email nvarchar(50),
Contact nvarchar(50),
TripType nvarchar(50),
LeavingFrom nvarchar(50),
GoingTo nvarchar(50),
Departing nvarchar(50),
Adults nvarchar(50),
Children nvarchar(50),
FlightType nvarchar(50),
Airline nvarchar(50),
TravelClass nvarchar(50),
Amount nvarchar(50)
)

create procedure spONEWAYFLIGHTS
@Name nvarchar(50),
@Email nvarchar(50),
@Contact nvarchar(50),
@TripType nvarchar(50),
@LeavingFrom nvarchar(50),
@GoingTo nvarchar(50),
@Departing nvarchar(50),
@Adults nvarchar(50),
@Children nvarchar(50),
@FlightType nvarchar(50),
@Airline nvarchar(50),
@TravelClass nvarchar(50),
@Amount nvarchar(50)
as
begin
insert into ONEWAYFLIGHTS(Name,Email,Contact,TripType,LeavingFrom,GoingTo,Departing,Adults,Children,FlightType,Airline,TravelClass,Amount)
values(@Name,@Email,@Contact,@TripType,@LeavingFrom,@GoingTo,@Departing,@Adults,@Children,@FlightType,@Airline,@TravelClass,@Amount)
end

create procedure spUpdateONEWAYFLIGHTS
@Id int,
@Name nvarchar(50),
@Email nvarchar(50),
@Contact nvarchar(50),
@TripType nvarchar(50),
@LeavingFrom nvarchar(50),
@GoingTo nvarchar(50),
@Departing nvarchar(50),
@Adults nvarchar(50),
@Children nvarchar(50),
@FlightType nvarchar(50),
@Airline nvarchar(50),
@TravelClass nvarchar(50),
@Amount nvarchar(50)
as
begin
update ONEWAYFLIGHTS set Name=@Name,Email=@Email,Contact=@Contact,TripType=@TripType,LeavingFrom=@LeavingFrom,GoingTo=@GoingTo,Departing=@Departing,Adults=@Adults,Children=@Children,FlightType=@FlightType,Airline=@Airline,TravelClass=@TravelClass,Amount=@Amount
where Id=@Id
end

create procedure spDeleteONEWAYFLIGHTS
@Id int
as
begin
delete from ONEWAYFLIGHTS
where Id=@Id
end

--FlightTickets
create table FLIGHTTICKETS
(
Id int Primary Key Identity(1,1),
PassengerName nvarchar(50),
Status nvarchar(50),
Contact nvarchar(50),
Airline nvarchar(50),
TravelClass nvarchar(50),
LeavingFrom nvarchar(50),
GoingTo nvarchar(50),
Email nvarchar(50),
Flight nvarchar(50),
Seat nvarchar(50),
CheckIn nvarchar(50),
Gate nvarchar(50),
Date nvarchar(50),
Barcode nvarchar(50)
)

create procedure spFLIGHTTICKETS
@PassengerName nvarchar(50),
@Status nvarchar(50),
@Contact nvarchar(50),
@Airline nvarchar(50),
@TravelClass nvarchar(50),
@LeavingFrom nvarchar(50),
@GoingTo nvarchar(50),
@Email nvarchar(50),
@Flight nvarchar(50),
@Seat nvarchar(50),
@CheckIn nvarchar(50),
@Gate nvarchar(50),
@Date nvarchar(50),
@Barcode nvarchar(50)
as
begin
insert into FLIGHTTICKETS(PassengerName,Status,Contact,Airline,TravelClass,LeavingFrom,GoingTo,Email,Flight,Seat,CheckIn,Gate,Date,Barcode)
values(@PassengerName,@Status,@Contact,@Airline,@TravelClass,@LeavingFrom,@GoingTo,@Email,@Flight,@Seat,@CheckIn,@Gate,@Date,@Barcode)
end

create procedure spDeleteFLIGHTTICKETS
@Id int
as
begin
delete from FLIGHTTICKETS
where Id=@Id
end


--Hotel Booking
create table HOTELBOOKING
(
Id int Primary Key Identity(1,1),
Hotel nvarchar(50),
Name nvarchar(50),
Email nvarchar(50),
Travelers nvarchar(50),
Rooms nvarchar(50),
RoomType nvarchar(50),
CheckIn nvarchar(50),
CheckOut nvarchar(50),
Contact nvarchar(50),
Amount nvarchar(50)
)

create procedure spHOTELBOOKING
@Hotel nvarchar(50),
@Name nvarchar(50),
@Email nvarchar(50),
@Travelers nvarchar(50),
@Rooms nvarchar(50),
@RoomType nvarchar(50),
@CheckIn nvarchar(50),
@CheckOut nvarchar(50),
@Contact nvarchar(50),
@Amount nvarchar(50)
as
begin
insert into HOTELBOOKING(Hotel,Name,Email,Travelers,Rooms,RoomType,CheckIn,CheckOut,Contact,Amount)
values(@Hotel,@Name,@Email,@Travelers,@Rooms,@RoomType,@CheckIn,@CheckOut,@Contact,@Amount)
end

create procedure spUpdateHOTELBOOKING
@Id int,
@Hotel nvarchar(50),
@Name nvarchar(50),
@Email nvarchar(50),
@Travelers nvarchar(50),
@Rooms nvarchar(50),
@RoomType nvarchar(50),
@CheckIn nvarchar(50),	
@CheckOut nvarchar(50),
@Contact nvarchar(50),
@Amount nvarchar(50)
as
begin
update HOTELBOOKING set Hotel=@Hotel,Name=@Name,Email=@Email,Travelers=@Travelers,Rooms=@Rooms,RoomType=@RoomType,CheckIn=@CheckIn,CheckOut=@CheckOut,Contact=@Contact,Amount=@Amount
where Id=@Id
end

create procedure spDeleteHOTELBOOKING
@Id int
as
begin
delete from HOTELBOOKING
where Id=@Id
end


--Payment Information
create table FLIGHTPAYMENTS
(
Id int Primary Key Identity(1,1),
Name nvarchar(50),
Email nvarchar(50),
PaymentMethod nvarchar(50),
CardNumber nvarchar(50),
CVV nvarchar(50),
ExpiryDate nvarchar(50),
Amount nvarchar(50),
)

create procedure spFLIGHTPAYMENTS
@Name nvarchar(50),
@Email nvarchar(50),
@PaymentMethod nvarchar(50),
@CardNumber nvarchar(50),
@CVV nvarchar(50),
@ExpiryDate nvarchar(50),
@Amount nvarchar(50)
as
begin
insert into FLIGHTPAYMENTS(Name,Email,PaymentMethod,CardNumber,CVV,ExpiryDate,Amount)
values(@Name,@Email,@PaymentMethod,@CardNumber,@CVV,@ExpiryDate,@Amount)
end

create procedure spUpdateFLIGHTPAYMENTS
@Id int,
@Name nvarchar(50),
@Email nvarchar(50),
@PaymentMethod nvarchar(50),
@CardNumber nvarchar(50),
@CVV nvarchar(50),
@ExpiryDate nvarchar(50),	
@Amount nvarchar(50)
as
begin
update FLIGHTPAYMENTS set Name=@Name,Email=@Email,PaymentMethod=@PaymentMethod,CardNumber=@CardNumber,CVV=@CVV,ExpiryDate=@ExpiryDate,Amount=@Amount
where Id=@Id
end

create procedure spDeleteFLIGHTPAYMENTS
@Id int
as
begin
delete from FLIGHTPAYMENTS
where Id=@Id
end

create table USERACTIVITYLOG(
Username nvarchar(50),
DateTimestamp datetime,
Action nvarchar(50),
IPAddress nvarchar(50),
HostName nvarchar(50)
Primary Key Clustered(Username asc,DateTimestamp asc)
with(Pad_Index = Off, Statistics_NoreCompute = Off, Ignore_Dup_Key = Off, Allow_Row_Locks = On, Allow_Page_Locks = On)
On [Primary]) On [Primary] 
Go
Set Ansi_Padding Off

create procedure spUSERACTIVITYLOG
@Username nvarchar(50),
@DateTimestamp datetime,
@Action nvarchar(50),
@IPAddress nvarchar(50),
@HostName nvarchar(50)
as
begin
insert into USERACTIVITYLOG(Username,DateTimestamp,Action,IPAddress,HostName)
values(@Username,@DateTimestamp,@Action,@IPAddress,@HostName)
end

create table ADMINISTRATORACTIVITYLOG(
AdministratorId nvarchar(50),
DateTimestamp datetime,
Action nvarchar(50),
IPAddress nvarchar(50),
HostName nvarchar(50)
Primary Key Clustered(AdministratorId asc,DateTimestamp asc)
with(Pad_Index = Off, Statistics_NoreCompute = Off, Ignore_Dup_Key = Off, Allow_Row_Locks = On, Allow_Page_Locks = On)
On [Primary]) On [Primary] 
Go
Set Ansi_Padding Off

create procedure spADMINISTRATORACTIVITYLOG
@AdministratorId nvarchar(50),
@DateTimestamp datetime,
@Action nvarchar(50),
@IPAddress nvarchar(50),
@HostName nvarchar(50)
as
begin
insert into ADMINISTRATORACTIVITYLOG(AdministratorId,DateTimestamp,Action,IPAddress,HostName)
values(@AdministratorId,@DateTimestamp,@Action,@IPAddress,@HostName)
end
