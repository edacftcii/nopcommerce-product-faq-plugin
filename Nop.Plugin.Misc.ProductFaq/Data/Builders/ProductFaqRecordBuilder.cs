using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.ProductFaq.Domain;

namespace Nop.Plugin.Misc.ProductFaq.Data.Builders
{
    public class ProductFaqRecordBuilder : NopEntityBuilder<ProductFaqRecord>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(ProductFaqRecord.ProductId)).AsInt32().NotNullable()
                .WithColumn(nameof(ProductFaqRecord.Question)).AsString(400).NotNullable()
                .WithColumn(nameof(ProductFaqRecord.Answer)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(ProductFaqRecord.DisplayOrder)).AsInt32().NotNullable()
                .WithColumn(nameof(ProductFaqRecord.Published)).AsBoolean().NotNullable();
        }
    }
}
