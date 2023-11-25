IF SCHEMA_ID(N'core') IS NULL EXEC(N'CREATE SCHEMA [core];');
GO


IF SCHEMA_ID(N'metadata') IS NULL EXEC(N'CREATE SCHEMA [metadata];');
GO


IF SCHEMA_ID(N'mes') IS NULL EXEC(N'CREATE SCHEMA [mes];');
GO


CREATE SEQUENCE [core].[data_type_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[document_note_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [metadata].[document_type_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [mes].[equipment_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [mes].[person_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[property_mapping_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[property_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[property_value_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE TABLE [access_right] (
    [id] int NOT NULL IDENTITY,
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [code] nvarchar(max) NOT NULL,
    [name] nvarchar(max) NOT NULL,
    [category] nvarchar(max) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_access_right] PRIMARY KEY ([id])
);
GO


CREATE TABLE [core].[document] (
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [revision] int NOT NULL,
    [state] smallint NOT NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [is_deleted] bit NOT NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document] PRIMARY KEY ([document_type_id], [document_id])
);
GO


CREATE TABLE [metadata].[document_type] (
    [id] int NOT NULL DEFAULT (nextval('metadata.document_type_seq'::regclass)),
    [state] smallint NOT NULL,
    [flags] smallint NOT NULL,
    [code] varchar(64) NOT NULL,
    [name] nvarchar(1024) NOT NULL,
    [description] nvarchar(max) NULL,
    [image_name] varchar(256) NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document_type] PRIMARY KEY ([id])
);
GO


CREATE TABLE [mes].[equipment] (
    [id] int NOT NULL DEFAULT (nextval('mes.equipment_seq'::regclass)),
    [state] smallint NOT NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_equipment] PRIMARY KEY ([id])
);
GO


CREATE TABLE [mes].[person] (
    [id] int NOT NULL DEFAULT (nextval('mes.person_seq'::regclass)),
    [state] smallint NOT NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_person] PRIMARY KEY ([id])
);
GO


CREATE TABLE [core].[document_note] (
    [id] int NOT NULL DEFAULT (nextval('core.document_note_seq'::regclass)),
    [state] smallint NOT NULL,
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [note] nvarchar(max) NOT NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document_note] PRIMARY KEY ([id]),
    CONSTRAINT [fk_document_note__document] FOREIGN KEY ([document_type_id], [document_id]) REFERENCES [core].[document] ([document_type_id], [document_id]) ON DELETE CASCADE
);
GO


CREATE TABLE [core].[data_type] (
    [id] int NOT NULL DEFAULT (nextval('core.data_type_seq'::regclass)),
    [state] smallint NOT NULL,
    [code] varchar(64) NOT NULL,
    [name] nvarchar(1024) NOT NULL,
    [kind] smallint NOT NULL,
    [document_type_id] int NULL,
    CONSTRAINT [pk_data_type] PRIMARY KEY ([id]),
    CONSTRAINT [fk_data_type__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [metadata].[document_type] ([id])
);
GO


CREATE TABLE [document_state] (
    [id] int NOT NULL IDENTITY,
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [document_type_id] int NOT NULL,
    [value] smallint NOT NULL,
    [code] nvarchar(max) NOT NULL,
    [name] nvarchar(max) NOT NULL,
    [description] nvarchar(max) NULL,
    [laber_color] nvarchar(max) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    [document_state_id] int NULL,
    CONSTRAINT [pk_document_state] PRIMARY KEY ([id]),
    CONSTRAINT [fk_document_state_document_state_document_state_id] FOREIGN KEY ([document_state_id]) REFERENCES [document_state] ([id]),
    CONSTRAINT [fk_document_state_document_type_document_type_id] FOREIGN KEY ([document_type_id]) REFERENCES [metadata].[document_type] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [core].[property] (
    [id] int NOT NULL DEFAULT (nextval('core.property_seq'::regclass)),
    [state] smallint NOT NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [flags] int NOT NULL,
    [parent_id] int NULL,
    [data_type_id] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_property] PRIMARY KEY ([id]),
    CONSTRAINT [fk_property_data_type_data_type_id] FOREIGN KEY ([data_type_id]) REFERENCES [core].[data_type] ([id]) ON DELETE CASCADE,
    CONSTRAINT [fk_property_property_parent_id] FOREIGN KEY ([parent_id]) REFERENCES [core].[property] ([id])
);
GO


CREATE TABLE [transition_template] (
    [id] int NOT NULL IDENTITY,
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [document_type_id] int NOT NULL,
    [from_state_value] smallint NOT NULL,
    [to_state_value] smallint NOT NULL,
    [access_right_id] int NOT NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    [from_state_id] int NULL,
    [to_state_id] int NULL,
    CONSTRAINT [pk_transition_template] PRIMARY KEY ([id]),
    CONSTRAINT [fk_transition_template_access_right_access_right_id] FOREIGN KEY ([access_right_id]) REFERENCES [access_right] ([id]) ON DELETE CASCADE,
    CONSTRAINT [fk_transition_template_document_state_from_state_id] FOREIGN KEY ([from_state_id]) REFERENCES [document_state] ([id]),
    CONSTRAINT [fk_transition_template_document_state_to_state_id] FOREIGN KEY ([to_state_id]) REFERENCES [document_state] ([id]),
    CONSTRAINT [fk_transition_template_document_type_document_type_id] FOREIGN KEY ([document_type_id]) REFERENCES [metadata].[document_type] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [core].[property_mapping] (
    [id] int NOT NULL DEFAULT (nextval('core.property_mapping_seq'::regclass)),
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [code] nvarchar(max) NOT NULL,
    [name] nvarchar(max) NOT NULL,
    [document_type_id] int NOT NULL,
    [property_id] int NOT NULL,
    [ordinal] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_property_mapping] PRIMARY KEY ([id]),
    CONSTRAINT [fk_property_mapping__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [metadata].[document_type] ([id]),
    CONSTRAINT [fk_property_mapping__property] FOREIGN KEY ([property_id]) REFERENCES [core].[property] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [core].[property_value] (
    [id] bigint NOT NULL DEFAULT (nextval('core.property_value_seq'::regclass)),
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [revision] int NOT NULL,
    [property_id] int NOT NULL,
    [kind] smallint NOT NULL,
    [int_value] int NULL,
    [date_time_value] datetime2 NULL,
    [leading_string_value] nvarchar(300) NULL,
    [trailing_string_value] nvarchar(max) NULL,
    [decimal_value] decimal(18,2) NULL,
    [double_value] float NULL,
    [binary_value] varbinary(max) NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_property_value] PRIMARY KEY ([id]),
    CONSTRAINT [fk_property_value__document] FOREIGN KEY ([document_type_id], [document_id]) REFERENCES [core].[document] ([document_type_id], [document_id]),
    CONSTRAINT [fk_property_value__property] FOREIGN KEY ([property_id]) REFERENCES [core].[property] ([id])
);
GO


