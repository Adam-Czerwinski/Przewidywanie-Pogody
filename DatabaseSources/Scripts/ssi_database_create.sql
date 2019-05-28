drop database if exists forecast_weather;
create database forecast_weather;
use forecast_weather;

create table cities
(
    id_cities int unsigned not null auto_increment,
		primary key(id_cities),
    name char(42) not null,
	region enum
		(
		"C","N", "E", "S", "W"
		) not null,
	is_station boolean not null
)
    engine=innodb
    default character set utf8 collate utf8_unicode_ci;
	
	
create table weather_data
(
    id_weather_data int unsigned not null auto_increment,
		primary key(id_weather_data),
    city int unsigned not null,
		foreign key(city) references cities(id_cities),
	date_ date not null,
	hour_ time not null,
    temperature double not null,
	humidity int not null,
	wind_direction enum
		(
		"C", 
		"N", "NNE", "NE", "ENE", 
		"E", "ESE", "SE", "SSE",
		"S", "SSW", "SW", "WSW",
		"W", "WNW", "NW", "NNW"
		) not null,
	wind_speed decimal not null,
	cludy decimal not null,
	visibility decimal not null,
	data_type enum
		(
		"Learning_data", "Testing_data", "User_input_data", "Recursive_data"
		) not null
)
    engine=innodb
    default character set utf8 collate utf8_unicode_ci;
	
create table activation_functions
(
	id_activation_functions int unsigned not null auto_increment,
		primary key(id_activation_functions),
	name_activation_functions char(60) not null
)
	engine=innodb
    default character set utf8 collate utf8_unicode_ci;
	
create table generations
(
    id_generations int unsigned not null auto_increment,
		primary key(id_generations),
    neurons_in int not null,
	neurons_hidden int not null,
	neurons_out int not null,
	learning_rate double not null,
	activation_function int unsigned not null,
		foreign key(activation_function) references activation_functions(id_activation_functions)
)
    engine=innodb
    default character set utf8 collate utf8_unicode_ci;
	
create table weight
(
    id_weight int unsigned not null auto_increment,
		primary key(id_weight),
    weight_value text not null
)
    engine=innodb
    default character set utf8 collate utf8_unicode_ci;
	
create table learning_process
(
    id_learning_process int unsigned not null auto_increment,
		primary key(id_learning_process),
	generation int unsigned not null,
		foreign key(generation) references generations(id_generations),
	weight int unsigned not null,
		foreign key(weight) references weight(id_weight),
	learning_time time not null,
	epoch int not null,
	total_error double not null,
	is_learned boolean not null
)
    engine=innodb
    default character set utf8 collate utf8_unicode_ci;