create or replace view B1_ItemGroupCODE_View as 
SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='G001' AND GROUP_USED='Y';

create or replace view B2_marl_View as
select arl.pat_no pat_no, max(arl.result_date) mrd 
from a_result_log arl
inner join B1_ItemGroupCODE_View igc on arl.RESULT_CODE = igc.RITEM_CODE 
WHERE arl.RESULT_VER=0 
group by arl.pat_no;

create or replace view B3_BI_View as
select arl.pat_no, arl.result_date, arl.result_code, arl.RESULT_VALUE_T, arl.RESULT_VALUE_N AS RESULT_VALUE, ars.RITEM_LOW1, ars.RITEM_HIGH1,
(case when arl.RESULT_VALUE_N > ars.RITEM_HIGH1 then 1 when arl.RESULT_VALUE_N < ars.RITEM_LOW1 then 1 else 0 end) AS BI 
from a_result_log arl 
inner join B2_marl_View marl on arl.pat_no = marl.pat_no and arl.result_date = marl.mrd 
inner join a_ritem_setup ars on arl.RESULT_CODE=ars.RITEM_CODE
AND arl.RESULT_CODE IN ('4003','4008','4021','4023','4027','4030','4050','5018')
ORDER BY pat_no, result_date desc, result_code;
create or replace view BI_SUM_View as
SELECT pat_no,SUM(BI) AS BI FROM B3_BI_View 
GROUP BY pat_no;