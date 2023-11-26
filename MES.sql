IF SCHEMA_ID(N'sec') IS NULL EXEC(N'CREATE SCHEMA [sec];');
GO


IF SCHEMA_ID(N'acc') IS NULL EXEC(N'CREATE SCHEMA [acc];');
GO


IF SCHEMA_ID(N'dic') IS NULL EXEC(N'CREATE SCHEMA [dic];');
GO


IF SCHEMA_ID(N'core') IS NULL EXEC(N'CREATE SCHEMA [core];');
GO


IF SCHEMA_ID(N'doc') IS NULL EXEC(N'CREATE SCHEMA [doc];');
GO


IF SCHEMA_ID(N'meta') IS NULL EXEC(N'CREATE SCHEMA [meta];');
GO


IF SCHEMA_ID(N'mes') IS NULL EXEC(N'CREATE SCHEMA [mes];');
GO


CREATE SEQUENCE [sec].[access_right_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [acc].[account_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [dic].[country_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [dic].[currency_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [doc].[document_attachment_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[document_note_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [meta].[document_state_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[document_transition_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [meta].[document_type_seq] START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [doc].[file_blob_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [mes].[hierarchy_level_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [mes].[hierarchy_scope_mapping_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [mes].[hierarchy_scope_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [acc].[operation_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[property_mapping_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[property_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[property_value_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [mes].[resource_class_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [mes].[resource_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [core].[simple_document_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [acc].[subconto_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE SEQUENCE [meta].[transition_template_seq] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO


CREATE TABLE [sec].[access_right] (
    [id] int NOT NULL DEFAULT (next value for [sec].[access_right_seq]),
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [code] varchar(128) NOT NULL,
    [name] nvarchar(1024) NOT NULL,
    [category] nvarchar(1024) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_access_right] PRIMARY KEY ([id]),
    CONSTRAINT [ak_access_right_code] UNIQUE ([code])
);
GO


CREATE TABLE [acc].[account] (
    [id] int NOT NULL DEFAULT (next value for [acc].[account_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NOT NULL,
    [name] nvarchar(1024) NULL,
    [flags] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_account] PRIMARY KEY ([id]),
    CONSTRAINT [ak_account_code] UNIQUE ([code])
);
GO


CREATE TABLE [dic].[country] (
    [id] int NOT NULL DEFAULT (next value for [dic].[country_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NOT NULL,
    [name] nvarchar(1024) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_country] PRIMARY KEY ([id]),
    CONSTRAINT [ak_country_code] UNIQUE ([code])
);
GO


CREATE TABLE [dic].[currency] (
    [id] int NOT NULL DEFAULT (next value for [dic].[currency_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NOT NULL,
    [name] nvarchar(1024) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_currency] PRIMARY KEY ([id]),
    CONSTRAINT [ak_currency_code] UNIQUE ([code])
);
GO


CREATE TABLE [core].[document] (
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [revision] int NOT NULL,
    [state] smallint NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document] PRIMARY KEY ([document_type_id], [document_id])
);
GO


CREATE TABLE [meta].[document_type] (
    [id] int NOT NULL DEFAULT (next value for [meta].[document_type_seq]),
    [state] smallint NOT NULL,
    [flags] smallint NOT NULL,
    [code] varchar(64) NOT NULL,
    [name] nvarchar(1024) NOT NULL,
    [description] nvarchar(max) NULL,
    [image_name] varchar(1024) NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document_type] PRIMARY KEY ([id]),
    CONSTRAINT [ak_document_type_code] UNIQUE ([code])
);
GO


CREATE TABLE [doc].[file_blob] (
    [id] int NOT NULL DEFAULT (next value for [doc].[file_blob_seq]),
    [state] smallint NOT NULL,
    [name] nvarchar(max) NOT NULL,
    [original_file_name] nvarchar(max) NOT NULL,
    [extension] nvarchar(max) NULL,
    [content_type] nvarchar(max) NOT NULL,
    [relative_path] nvarchar(max) NOT NULL,
    [hash_value] nvarchar(max) NOT NULL,
    [comments] nvarchar(max) NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_file_blob] PRIMARY KEY ([id])
);
GO


CREATE TABLE [mes].[hierarchy_level] (
    [id] int NOT NULL DEFAULT (next value for [mes].[hierarchy_level_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] nvarchar(max) NULL,
    [name] nvarchar(max) NULL,
    [flags] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_hierarchy_level] PRIMARY KEY ([id])
);
GO


CREATE TABLE [core].[document_note] (
    [id] int NOT NULL DEFAULT (next value for [core].[document_note_seq]),
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


CREATE TABLE [acc].[operation] (
    [id] bigint NOT NULL DEFAULT (next value for [acc].[operation_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [document_type_id] int NULL,
    [document_id] int NULL,
    [operation_date] date NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_operation] PRIMARY KEY ([id]),
    CONSTRAINT [fk_operation__document] FOREIGN KEY ([document_type_id], [document_id]) REFERENCES [core].[document] ([document_type_id], [document_id])
);
GO


CREATE TABLE [core].[data_type] (
    [id] int NOT NULL,
    [state] smallint NOT NULL,
    [code] varchar(64) NOT NULL,
    [name] nvarchar(1024) NOT NULL,
    [kind] smallint NOT NULL,
    [document_type_id] int NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_data_type] PRIMARY KEY ([id]),
    CONSTRAINT [fk_data_type__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [meta].[document_type] ([id])
);
GO


CREATE TABLE [meta].[document_state] (
    [id] int NOT NULL DEFAULT (next value for [meta].[document_state_seq]),
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [document_type_id] int NOT NULL,
    [value] smallint NOT NULL,
    [code] varchar(64) NOT NULL,
    [name] nvarchar(1024) NOT NULL,
    [description] nvarchar(max) NULL,
    [laber_color] varchar(32) NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document_state] PRIMARY KEY ([id]),
    CONSTRAINT [ak_document_state_value] UNIQUE ([document_type_id], [value]),
    CONSTRAINT [fk_document_state__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [meta].[document_type] ([id])
);
GO


CREATE TABLE [mes].[resource] (
    [id] int NOT NULL DEFAULT (next value for [mes].[resource_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] nvarchar(max) NULL,
    [name] nvarchar(max) NULL,
    [uid] uniqueidentifier NULL,
    [document_type_id] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_resource] PRIMARY KEY ([id]),
    CONSTRAINT [fk_resource__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [meta].[document_type] ([id])
);
GO


CREATE TABLE [mes].[resource_class] (
    [id] int NOT NULL DEFAULT (next value for [mes].[resource_class_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [resource_type_id] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_resource_class] PRIMARY KEY ([id]),
    CONSTRAINT [fk_resource_class__resource_type] FOREIGN KEY ([resource_type_id]) REFERENCES [meta].[document_type] ([id])
);
GO


CREATE TABLE [core].[simple_document] (
    [id] int NOT NULL DEFAULT (next value for [core].[simple_document_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] nvarchar(max) NULL,
    [name] nvarchar(max) NULL,
    [document_type_id] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_simple_document] PRIMARY KEY ([id]),
    CONSTRAINT [ak_simple_document_document_type_id_id] UNIQUE ([document_type_id], [id]),
    CONSTRAINT [fk_simple_document__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [meta].[document_type] ([id])
);
GO


CREATE TABLE [acc].[subconto] (
    [id] int NOT NULL DEFAULT (next value for [acc].[subconto_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NOT NULL,
    [name] nvarchar(1024) NULL,
    [flags] int NOT NULL,
    [account_id] int NOT NULL,
    [document_type_id] int NULL,
    [ordinal] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_subconto] PRIMARY KEY ([id]),
    CONSTRAINT [ak_subconto_code] UNIQUE ([code]),
    CONSTRAINT [fk_subconto__account] FOREIGN KEY ([account_id]) REFERENCES [acc].[account] ([id]) ON DELETE CASCADE,
    CONSTRAINT [fk_subconto__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [meta].[document_type] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [doc].[document_attachment] (
    [id] int NOT NULL DEFAULT (next value for [doc].[document_attachment_seq]),
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [attachment_type] smallint NOT NULL,
    [file_id] int NULL,
    [comments] nvarchar(max) NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document_attachment] PRIMARY KEY ([id]),
    CONSTRAINT [fk_document_attachment__document] FOREIGN KEY ([document_type_id], [document_id]) REFERENCES [core].[document] ([document_type_id], [document_id]) ON DELETE CASCADE,
    CONSTRAINT [fk_document_attachment__file_blob] FOREIGN KEY ([file_id]) REFERENCES [doc].[file_blob] ([id])
);
GO


CREATE TABLE [mes].[hierarchy_scope] (
    [id] int NOT NULL DEFAULT (next value for [mes].[hierarchy_scope_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] nvarchar(max) NULL,
    [name] nvarchar(max) NULL,
    [uid] uniqueidentifier NULL,
    [path] varchar(1024) NOT NULL,
    [hierarch_level_id] int NOT NULL,
    [flags] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_hierarchy_scope] PRIMARY KEY ([id]),
    CONSTRAINT [fk_hierarchy_scope__hierarchy_level] FOREIGN KEY ([hierarch_level_id]) REFERENCES [mes].[hierarchy_level] ([id])
);
GO


CREATE TABLE [core].[document_note_user_state] (
    [document_note_id] int NOT NULL,
    [user_id] int NOT NULL,
    [is_read] bit NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document_note_user_state] PRIMARY KEY ([document_note_id], [user_id]),
    CONSTRAINT [fk_document_note_user_state__document_note] FOREIGN KEY ([document_note_id]) REFERENCES [core].[document_note] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [acc].[operation_part] (
    [operation_id] bigint NOT NULL,
    [side] smallint NOT NULL,
    [account_id] int NOT NULL,
    [amount] decimal(38,12) NOT NULL,
    [value] decimal(38,12) NOT NULL,
    [currency_id] int NOT NULL,
    [subconto00] int NULL,
    [subconto01] int NULL,
    [subconto02] int NULL,
    [subconto03] int NULL,
    [subconto04] int NULL,
    [subconto05] int NULL,
    [subconto06] int NULL,
    [subconto07] int NULL,
    [subconto08] int NULL,
    [subconto09] int NULL,
    [subconto10] int NULL,
    [subconto11] int NULL,
    [subconto12] int NULL,
    [subconto13] int NULL,
    [subconto14] int NULL,
    [subconto15] int NULL,
    [subconto16] int NULL,
    [subconto17] int NULL,
    [subconto18] int NULL,
    [subconto19] int NULL,
    [tag00] nvarchar(32) NULL,
    [tag01] nvarchar(32) NULL,
    [tag02] nvarchar(32) NULL,
    [tag03] nvarchar(32) NULL,
    CONSTRAINT [pk_operation_part] PRIMARY KEY ([operation_id], [side]),
    CONSTRAINT [fk_operation_part__account] FOREIGN KEY ([account_id]) REFERENCES [acc].[account] ([id]),
    CONSTRAINT [fk_operation_part__currency] FOREIGN KEY ([currency_id]) REFERENCES [dic].[currency] ([id]),
    CONSTRAINT [fk_operation_part__operation] FOREIGN KEY ([operation_id]) REFERENCES [acc].[operation] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [core].[property] (
    [id] int NOT NULL DEFAULT (next value for [core].[property_seq]),
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NOT NULL,
    [name] nvarchar(1024) NULL,
    [flags] int NOT NULL,
    [kind] smallint NOT NULL,
    [data_type_id] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_property] PRIMARY KEY ([id]),
    CONSTRAINT [ak_property_code] UNIQUE ([code]),
    CONSTRAINT [fk_property_data_type_data_type_id] FOREIGN KEY ([data_type_id]) REFERENCES [core].[data_type] ([id]) ON DELETE CASCADE,
    CONSTRAINT [fk_property_property_parent_id] FOREIGN KEY ([parent_id]) REFERENCES [core].[property] ([id])
);
GO


CREATE TABLE [core].[document_transition] (
    [id] bigint NOT NULL DEFAULT (next value for [core].[document_transition_seq]),
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [from_state_value] smallint NOT NULL,
    [to_state_value] smallint NOT NULL,
    [comments] nvarchar(max) NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_document_transition] PRIMARY KEY ([id]),
    CONSTRAINT [fk_document_transition__document] FOREIGN KEY ([document_type_id], [document_id]) REFERENCES [core].[document] ([document_type_id], [document_id]) ON DELETE CASCADE,
    CONSTRAINT [fk_document_transition__from_state] FOREIGN KEY ([document_type_id], [from_state_value]) REFERENCES [meta].[document_state] ([document_type_id], [value]),
    CONSTRAINT [fk_document_transition__to_state] FOREIGN KEY ([document_type_id], [to_state_value]) REFERENCES [meta].[document_state] ([document_type_id], [value])
);
GO


CREATE TABLE [meta].[transition_template] (
    [id] int NOT NULL DEFAULT (next value for [meta].[transition_template_seq]),
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [document_type_id] int NOT NULL,
    [from_state] smallint NOT NULL,
    [to_state] smallint NOT NULL,
    [access_right_id] int NOT NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_transition_template] PRIMARY KEY ([id]),
    CONSTRAINT [fk_transition_template__access_right] FOREIGN KEY ([access_right_id]) REFERENCES [sec].[access_right] ([id]),
    CONSTRAINT [fk_transition_template__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [meta].[document_type] ([id]),
    CONSTRAINT [fk_transition_template__from_state] FOREIGN KEY ([document_type_id], [from_state]) REFERENCES [meta].[document_state] ([document_type_id], [value]),
    CONSTRAINT [fk_transition_template__to_state] FOREIGN KEY ([document_type_id], [to_state]) REFERENCES [meta].[document_state] ([document_type_id], [value])
);
GO


CREATE TABLE [mes].[equipment] (
    [id] int NOT NULL,
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [uid] uniqueidentifier NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_equipment] PRIMARY KEY ([id]),
    CONSTRAINT [fk_equipment__resource] FOREIGN KEY ([id]) REFERENCES [mes].[resource] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [mes].[material_definition] (
    [id] int NOT NULL,
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [uid] uniqueidentifier NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_material_definition] PRIMARY KEY ([id]),
    CONSTRAINT [fk_material_definition__resource] FOREIGN KEY ([id]) REFERENCES [mes].[resource] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [mes].[person] (
    [id] int NOT NULL,
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [uid] uniqueidentifier NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_person] PRIMARY KEY ([id]),
    CONSTRAINT [fk_person__resource] FOREIGN KEY ([id]) REFERENCES [mes].[resource] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [mes].[physical_asset] (
    [id] int NOT NULL,
    [state] smallint NOT NULL,
    [revision] int NOT NULL,
    [parent_id] int NULL,
    [code] varchar(128) NULL,
    [name] nvarchar(1024) NULL,
    [uid] uniqueidentifier NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_physical_asset] PRIMARY KEY ([id]),
    CONSTRAINT [fk_physical_asset__resource] FOREIGN KEY ([id]) REFERENCES [mes].[resource] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [mes].[resource_class_mapping] (
    [resource_class_id] int NOT NULL,
    [resource_id] int NOT NULL,
    CONSTRAINT [pk_resource_class_mapping] PRIMARY KEY ([resource_class_id], [resource_id]),
    CONSTRAINT [fk_resource_class_mapping__resource] FOREIGN KEY ([resource_id]) REFERENCES [mes].[resource] ([id]) ON DELETE CASCADE,
    CONSTRAINT [fk_resource_class_mapping__resource_class] FOREIGN KEY ([resource_class_id]) REFERENCES [mes].[resource_class] ([id])
);
GO


CREATE TABLE [mes].[hierarchy_scope_mapping] (
    [id] int NOT NULL DEFAULT (next value for [mes].[hierarchy_scope_mapping_seq]),
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [hierarchy_scope_id] int NOT NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_hierarchy_scope_mapping] PRIMARY KEY ([id]),
    CONSTRAINT [fk_hierarchy_scope_mapping__document] FOREIGN KEY ([document_type_id], [document_id]) REFERENCES [core].[document] ([document_type_id], [document_id]),
    CONSTRAINT [fk_hierarchy_scope_mapping__hierarchy_scope] FOREIGN KEY ([hierarchy_scope_id]) REFERENCES [mes].[hierarchy_scope] ([id])
);
GO


CREATE TABLE [core].[property_mapping] (
    [id] int NOT NULL DEFAULT (next value for [core].[property_mapping_seq]),
    [state] smallint NOT NULL,
    [flags] int NOT NULL,
    [code] nvarchar(max) NOT NULL,
    [name] nvarchar(max) NOT NULL,
    [document_type_id] int NOT NULL,
    [resource_class_id] int NULL,
    [property_id] int NOT NULL,
    [ordinal] int NOT NULL,
    [comments] nvarchar(max) NULL,
    [created_on] datetimeoffset NOT NULL,
    [created_by] int NOT NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_property_mapping] PRIMARY KEY ([id]),
    CONSTRAINT [fk_property_mapping__document_type] FOREIGN KEY ([document_type_id]) REFERENCES [meta].[document_type] ([id]),
    CONSTRAINT [fk_property_mapping__property] FOREIGN KEY ([property_id]) REFERENCES [core].[property] ([id]) ON DELETE CASCADE,
    CONSTRAINT [fk_property_mapping__resource_class] FOREIGN KEY ([resource_class_id]) REFERENCES [mes].[resource_class] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [core].[property_value] (
    [id] bigint NOT NULL DEFAULT (next value for [core].[property_value_seq]),
    [document_type_id] int NOT NULL,
    [document_id] int NOT NULL,
    [revision] int NOT NULL,
    [property_id] int NOT NULL,
    [kind] smallint NOT NULL,
    [int_value] int NULL,
    [date_time_value] datetime2 NULL,
    [leading_string_value] nvarchar(300) NULL,
    [trailing_string_value] nvarchar(max) NULL,
    [decimal_value] decimal(38,12) NULL,
    [double_value] float NULL,
    [binary_value] varbinary(max) NULL,
    [modified_on] datetimeoffset NOT NULL,
    [modified_by] int NOT NULL,
    CONSTRAINT [pk_property_value] PRIMARY KEY ([id]),
    CONSTRAINT [fk_property_value__document] FOREIGN KEY ([document_type_id], [document_id]) REFERENCES [core].[document] ([document_type_id], [document_id]),
    CONSTRAINT [fk_property_value__property] FOREIGN KEY ([property_id]) REFERENCES [core].[property] ([id])
);
GO


