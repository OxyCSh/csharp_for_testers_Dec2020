﻿using LinqToDB.Mapping;

namespace AddressbookWebTests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupId { get; }

        [Column(Name = "id")]
        public string ContactId { get; }
    }
}