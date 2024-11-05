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
            var query = QueryMapper.MapQuery<UsersQuery, List<User>>(u => u.Users
                .With(
                u => u.BigProfilePicture.IsAliasFor("ProfilePicture", "size", 1024),
                u => u.SmallProfilePicture.IsAliasFor("ProfilePicture", "size", 64)));

            var usersNode = query.FindChild("Users");
            usersNode.Should().NotBeNull();

            usersNode.Nodes.Filtered.Should().HaveCount(3);

            var bigPp = usersNode.FindChild("BigProfilePicture");
            bigPp.Should().NotBeNull();
            bigPp.IsAliasFor.Should().Be("ProfilePicture");
            bigPp.Arguments.Should().HaveCount(1);
            bigPp.Arguments[0].Should().BeEquivalentTo(new KeyValuePair<string, object> ("size", 1024));

            var smallPp = usersNode.FindChild("SmallProfilePicture");
            smallPp.Should().NotBeNull();
            smallPp.IsAliasFor.Should().Be("ProfilePicture");
            smallPp.Arguments.Should().HaveCount(1);
            smallPp.Arguments[0].Should().BeEquivalentTo(new KeyValuePair<string, object>("size", 64));

            var rendered = query.ToString();

            rendered.Should().ContainAll("bigProfilePicture: ProfilePicture(size: 1024)", "smallProfilePicture: ProfilePicture(size: 64)");
        }

        [Fact]
        public void AliasCombinedWithArgument()
        {
            var query = QueryMapper.MapQuery<UsersQuery, List<User>>(u => u.Users
                .With(
                u => u.BigProfilePicture.IsAliasFor("ProfilePicture").Argument("size", 1024).With(i => i.Format),
                u => u.SmallProfilePicture.Argument("size", 64).IsAliasFor("ProfilePicture").With(i => i.Format)));

            var usersNode = query.FindChild("Users");
            usersNode.Should().NotBeNull();
                        
            usersNode.Nodes.Filtered.Should().HaveCount(4);

            var bigPp = usersNode.FindChild("BigProfilePicture");
            bigPp.Should().NotBeNull();
            bigPp.IsAliasFor.Should().Be("ProfilePicture");
            bigPp.Arguments.Should().HaveCount(1);
            bigPp.Arguments[0].Should().BeEquivalentTo(new KeyValuePair<string, object>("size", 1024));

            var smallPp = usersNode.FindChild("SmallProfilePicture");
            smallPp.Should().NotBeNull();
            smallPp.IsAliasFor.Should().Be("ProfilePicture");
            smallPp.Arguments.Should().HaveCount(1);
            smallPp.Arguments[0].Should().BeEquivalentTo(new KeyValuePair<string, object>("size", 64));

            var rendered = query.ToString();

            rendered.Should().ContainAll("bigProfilePicture: ProfilePicture(size: 1024)", "smallProfilePicture: ProfilePicture(size: 64)");
        }

        [Fact]
        public void AliasCouldBeAssignedByAttribute()
        {
            var query = QueryMapper.MapQuery<UsersQuery, List<User>>(u => u.Users.With());

            var usersNode = query.FindChild("Users");
            usersNode.Should().NotBeNull();

            var userNameNode = usersNode.FindChild("UserName");
            userNameNode.Should().NotBeNull();
            userNameNode.IsAliasFor.Should().Be("email");

            var rendered = query.ToString();
        }

        class User
        {
            public string Id { get; set; }
            public Picture BigProfilePicture { get; set; }
            public Picture SmallProfilePicture { get; set; }

            [Gql(IsAliasFor = "email")]
            public string UserName { get; set; }
        }

        class UsersQuery
        {
            public List<User> Users { get; set; }
        }

        class Picture
        {
            public string Url { get; set; }
            public ImageFormat Format { get; set; }
        }

        class ImageFormat
        {
            public string Extension { get; set; }
            public string Name { get; set; }
        }
    }
}

