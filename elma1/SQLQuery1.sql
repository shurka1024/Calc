select op.name, AVG(opr.ExecTime_ms)
from OperationResult as opr
join Operation as op ON op.Id = opr.OperationId
where opr.ArgumentCount = 1
group by op.name
having AVG(opr.ExecTime_ms) > 100000