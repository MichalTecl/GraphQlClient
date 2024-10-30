using FluentAssertions;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;

namespace MTecl.GraphQlClient.IntegrationTests
{
    public class QueryMappingWithAliasesTests
    {
        [Fact]
        public void AliasWithArgumentPassedAsNameValue()
        {
            var query = QueryMapper.MapQuery<UsersQuery>(u => u.Users
                .With(
                u => u.BigProfilePictureUrl.IsAliasFor("profilePictureUrl", "size", 1024),
                u => u.SmallProfilePictureUrl.IsAliasFor("profilePictureUrl", "size", 64)));

            var usersNode = query.FindChild("Users");
            usersNode.Should().NotBeNull();

            usersNode.Nodes.Filtered.Should().HaveCount(3);

            var bigPp = usersNode.FindChild("BigProfilePictureUrl");
            bigPp.Should().NotBeNull();
            bigPp.IsAliasFor.Should().Be("profilePictureUrl");
            bigPp.Arguments.Should().HaveCount(1);
            bigPp.Arguments[0].Should().BeEquivalentTo(new KeyValuePair<string, object> ("size", 1024));

            var smallPp = usersNode.FindChild("SmallProfilePictureUrl");
            smallPp.Should().NotBeNull();
            smallPp.IsAliasFor.Should().Be("profilePictureUrl");
            smallPp.Arguments.Should().HaveCount(1);
            smallPp.Arguments[0].Should().BeEquivalentTo(new KeyValuePair<string, object>("size", 64));

            var rendered = query.ToString();
        }

        [Fact]
        public void AliasCombinedWithArgument()
        {
            var query = QueryMapper.MapQuery<UsersQuery>(u => u.Users
                .With(
                u => u.BigProfilePictureUrl.IsAliasFor("profilePictureUrl").Argument("size", 1024),
                u => u.SmallProfilePictureUrl.IsAliasFor("profilePictureUrl").Argument("size", 64)));

            var rendered = query.ToString();
        }

        class User
        {
            public string Id { get; set; }
            public string BigProfilePictureUrl { get; set; }
            public string SmallProfilePictureUrl { get; set; }
        }

        class UsersQuery
        {
            public List<User> Users { get; set; }
        }
       
    }
}

