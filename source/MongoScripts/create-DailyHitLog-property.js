db.users.update({ 'DailyHitLog': { $exists: false } },
    { $set: { DailyHitLog: [] } },
    { multi: true }
);