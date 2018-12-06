DROP TABLE IF EXISTS `leafsegment`;
CREATE TABLE `leafsegment`  (
  `biz_tag` varchar(255) NULL DEFAULT NULL,
  `max_id` bigint(20) NULL DEFAULT 0,
  `step` int(11) NULL DEFAULT 5000,
  `desc` varchar(255)  NULL DEFAULT NULL,
  `update_time` datetime(0) NULL DEFAULT now()
);


INSERT INTO `leafsegment` VALUES ('test', 0, 5000, '测试', '2018-12-06 23:32:11');