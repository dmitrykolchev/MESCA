// <copyright file="MesSqlServerDbContext.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Xobex.Entities.Core;
using Xobex.Entities.Metadata;
using Xobex.Entities.Security;
using Xobex.Infrastructure.EntityFramework;
using Xobex.Infrastucture.Configuration.Core;
using Xobex.Infrastucture.Configuration.Metadata;
using Xobex.Infrastucture.Configuration.Security;
using Xobex.Mes.Application;
using Xobex.Mes.Entities.Accounting;
using Xobex.Mes.Entities.Core;
using Xobex.Mes.Entities.Dictionaries;
using Xobex.Mes.Entities.DocumentManagement;
using Xobex.Mes.Entities.Resources;
using Xobex.Mes.Infrastucture.Configuration.Accounting;
using Xobex.Mes.Infrastucture.Configuration.Core;
using Xobex.Mes.Infrastucture.Configuration.Dictionaries;
using Xobex.Mes.Infrastucture.Configuration.DocumentManagement;
using Xobex.Mes.Infrastucture.Configuration.Resources;

namespace Xobex.Mes.Infrastucture.Database;

public class MesSqlServerDbContext : DbContextBase, IMesDbContext, ISqlServer
{
    public MesSqlServerDbContext(DbContextOptions options) : base(options)
    {
    }
    //
    // Security
    // 
    [EntityConfiguration(typeof(AccessRightConfiguration))]
    public DbSet<AccessRight> AccessRight { get; set; }
    //
    // Metadata
    //
    [EntityConfiguration(typeof(DocumentTypeConfiguration))]
    public DbSet<DocumentType> DocumentType { get; set; }
    [EntityConfiguration(typeof(DocumentStateConfiguration))]
    public DbSet<DocumentState> DocumentState { get; set; }
    [EntityConfiguration(typeof(TransitionTemplateConfiguration))]
    public DbSet<TransitionTemplate> TransitionTemplate { get; set; }
    //
    // Common/Core objects
    // 
    [EntityConfiguration(typeof(DataTypeConfiguration))]
    public DbSet<DataType> DataType { get; set; }
    [EntityConfiguration(typeof(DocumentConfiguration))]
    public DbSet<Document> Document { get; set; }
    [EntityConfiguration(typeof(DocumentTransitionConfiguration))]
    public DbSet<Document> DocumentTransition { get; set; }
    [EntityConfiguration(typeof(DocumentNoteConfiguration))]
    public DbSet<DocumentNote> DocumentNote { get; set; }
    [EntityConfiguration(typeof(DocumentNoteUserStateConfiguration))]
    public DbSet<DocumentNoteUserState> GetDocumentNoteUserState { get; set; }
    [EntityConfiguration(typeof(SimpleDocumentConfiguration))]
    public DbSet<SimpleDocument> SimpleDocument { get; set; }
    [EntityConfiguration(typeof(PropertyConfiguration))]
    public DbSet<Property> Property { get; set; }
    [EntityConfiguration(typeof(PropertyMappingConfiguration))]
    public DbSet<PropertyMapping> PropertyMapping { get; set; }
    [EntityConfiguration(typeof(PropertyValueConfiguration))]
    public DbSet<PropertyValue> PropertyValue { get; set; }
    //
    // Accounting
    //
    [EntityConfiguration(typeof(AccountConfiguration))]
    public DbSet<Account> Account { get; set; }
    [EntityConfiguration(typeof(SubcontoConfiguration))]
    public DbSet<Subconto> Subconto { get; set; }
    [EntityConfiguration(typeof(OperationConfiguration))]
    public DbSet<Operation> Operation { get; set; }
    [EntityConfiguration(typeof(OperationPartConfiguration))]
    public DbSet<OperationPart> OperationPart { get; set; }
    //
    // Справочники
    //
    [EntityConfiguration(typeof(CurrencyConfiguration))]
    public DbSet<Currency> Currency { get; set; }
    [EntityConfiguration(typeof(CountryConfiguration))]
    public DbSet<Country> Country { get; set; }
    // 
    // Document Management
    //
    [EntityConfiguration(typeof(FileBlobConfiguration))]
    public DbSet<FileBlob> FileBlob { get; set; }
    [EntityConfiguration(typeof(DocumentAttachmentConfiguration))]
    public DbSet<DocumentAttachment> DocumentAttachment { get; set; }
    //
    // Resources
    //
    [EntityConfiguration(typeof(HierarchyLevelConfiguration))]
    public DbSet<HierarchyLevel> HierarchyLevel { get; set; }
    [EntityConfiguration(typeof(HierarchyScopeConfiguration))]
    public DbSet<HierarchyScope> HierarchyScope { get; set; }
    [EntityConfiguration(typeof(HierarchyScopeMappingConfiguration))]
    public DbSet<HierarchyScopeMapping> HierarchyScopeMapping { get; set; }
    [EntityConfiguration(typeof(ResourceClassConfiguration))]
    public DbSet<ResourceClass> ResourceClass { get; set; }
    [EntityConfiguration(typeof(ResourceClassMappingConfiguration))]
    public DbSet<ResourceClassMapping> ResourceClassMapping { get; set; }
    [EntityConfiguration(typeof(ResourceConfiguration))]
    public DbSet<Resource> Resource { get; set; }
    [EntityConfiguration(typeof(MaterialDefinitionConfiguration))]
    public DbSet<MaterialDefinition> MaterialDefinition { get; set; }
    [EntityConfiguration(typeof(PhysicalAssetConfiguration))]
    public DbSet<PhysicalAsset> PhysicalAsset { get; set; }
    [EntityConfiguration(typeof(EquipmentConfiguration))]
    public DbSet<Equipment> Equipment { get; set; }
    [EntityConfiguration(typeof(PersonConfiguration))]
    public DbSet<Person> Person { get; set; }

}
