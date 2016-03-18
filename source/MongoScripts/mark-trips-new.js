db.getCollection('trips').update({}, {$set:{Status:'New'}}, {multi:true});
db.getCollection('tripDrops').update({}, {$set:{IsDelivered:false}}, {multi:true});