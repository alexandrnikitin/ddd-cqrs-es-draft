using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using Octo.Core.Configuration;
using Octo.Core.Cqrs;
using Octo.Core.Cqrs.Common;
using Octo.Core.EventSourcing;

namespace Octo.Azure.EventSourcing.TableStorage
{
    public class AzureTableStorageEventStore<TAggregateRoot> : IEventStore<TAggregateRoot>
    {
        private readonly CloudTable _table;

        static AzureTableStorageEventStore()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(Constants.StorageConnectionString));
            var tableClient = storageAccount.CreateCloudTableClient();
            var tableName = typeof(TAggregateRoot).Name;
            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
        }

        public AzureTableStorageEventStore()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(Constants.StorageConnectionString));
            var tableClient = storageAccount.CreateCloudTableClient();
            var tableName = typeof(TAggregateRoot).Name;
            _table = tableClient.GetTableReference(tableName);
        }

        public IEnumerable<ICommittableEvent> GetEvents(Guid aggregateRootId)
        {
            var query = new TableQuery<EventTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, aggregateRootId.ToString()));


            List<EventTableEntity> eventTableEntities;
            
            eventTableEntities = _table.ExecuteQuery(query).ToList();

            eventTableEntities = eventTableEntities.OrderBy(e => long.Parse(e.RowKey)).ToList();

            return eventTableEntities.Select(
                entity =>
                new CommittableEvent(
                    Utility.DeJsonize(entity.Event, entity.EventType) as IEvent,
                    long.Parse(entity.RowKey),
                    entity.DateTime));
        }

        public void Insert(IEnumerable<ICommittableEvent> events)
        {
            var eventsToSave = events.ToList();

            if (eventsToSave.Count == 1)
            {
                var eventTableEntity = new EventTableEntity(eventsToSave.Single());
                var insertOperation = TableOperation.Insert(eventTableEntity);
                _table.Execute(insertOperation);
            }
            else
            {
                var batchOperation = new TableBatchOperation();
                foreach (var eventToSave in eventsToSave)
                {
                    var eventTableEntity = new EventTableEntity(eventToSave);
                    batchOperation.Insert(eventTableEntity);
                }

                _table.ExecuteBatch(batchOperation);
            }
        }
    }
}