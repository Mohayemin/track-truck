db.clients.update({ _id: { $not: { $eq: '' } } },
    { $set: { IsDeleted: false } },
    { multi: true }
);