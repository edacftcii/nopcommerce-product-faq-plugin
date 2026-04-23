using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.ProductFaq.Domain;

namespace Nop.Plugin.Misc.ProductFaq.Data.Migrations
{
    [NopMigration("2026/04/21 16:45:00:0000000", "Misc.ProductFaq base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<ProductFaqRecord>();
        }
    }
}
