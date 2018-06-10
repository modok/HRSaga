using System;
using System.Collections.Generic;
using hrSaga.core.infra;
using Xunit;

namespace hrSaga.test.unit.core.infra
{
    public class DataStoreSpec : IDisposable
    {
        DataStore _dataStore;

        class TestEntity : BaseEntity
        {
            public string Value { get; set; }

            public TestEntity(Guid id) : base(id)
            {
            }
        }

        public DataStoreSpec()
        {
            _dataStore = new DataStore();
        }

        [Fact]
        public void It_Should_Be_Possible_To_Insert_And_Get_An_Entity()
        {
            var id = Guid.NewGuid();

            _dataStore.Insert(new TestEntity(id) { Value = "Test1" });
            var firstEntity = _dataStore.GetFirst<TestEntity>();
            var entity = _dataStore.Get<TestEntity>(id);

            Assert.Equal("Test1", firstEntity.Value);
            Assert.Equal("Test1", entity.Value);
        }

        [Fact]
        public void It_Should_Not_Allow_The_Same_Id_Twice()
        {
            var id = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() =>
            {
                _dataStore.Insert(new TestEntity(id) { Value = "Test1" });
                _dataStore.Insert(new TestEntity(id) { Value = "Test2" });
            });
        }

        [Fact]
        public void It_Should_Be_Possible_To_Update_An_Entity()
        {
            var id = Guid.NewGuid();

            _dataStore.Insert(new TestEntity(id) { Value = "Test1" });
            _dataStore.Update(new TestEntity(id) { Value = "Test2" });
            var entity = _dataStore.Get<TestEntity>(id);

            Assert.Equal("Test2", entity.Value);
        }

        [Fact]
        public void It_Should_Be_Possible_To_Delete_An_Entity()
        {
            var id = Guid.NewGuid();

            _dataStore.Insert(new TestEntity(id) { Value = "Test1" });
            _dataStore.Delete<TestEntity>(id);

            Assert.Throws<KeyNotFoundException>(() =>
            {
                _dataStore.Get<TestEntity>(id);
            });
        }

        public void Dispose()
        {
        }
    }
}
