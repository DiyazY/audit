# audit
audit

## temp

 "C:\Program Files\MongoDB\Server\3.6\bin\mongod.exe" --dbpath D:\rest\mongo

 "C:\Program Files\MongoDB\Server\3.6\bin\mongo.exe"

var body = dbEntity?.FirstOrDefault()?.Body;
var diff = Diff.Get(body?.ToJson(), model?.Body?.ToJson());

var patch = Diff.Patch(body.ToJson(), diff);
var unpatch = Diff.Unpatch(model?.Body?.ToJson(), diff);