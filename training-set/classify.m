% Garrett Scholtes
% 2017-01-29
%
% Try to perform very basic binary classification on our training set.  
% This is meant as a benchmark to see how granular we can expect to get 
% with this dataset.  

clear all;

data = xlsread('ratings\formatted\garrett.xlsx');

% Which subset of bextract values?
bextract = 9:21; % 9:21 -> best choice of bextract data? Not sure

% Make positivity, intensity, confidence binary 
% (Try by median next)
data(:,2) = data(:,2) > median(data(:,2)); % 0;
data(:,3) = data(:,3) > median(data(:,3)); % 0;
data(:,4) = data(:,4) > median(data(:,4)); % 0.5;

% Seed
rng(2017);

permutation = randperm(size(data, 1));
% Leave 25 for test
TRAIN_SIZE = 55;
train_set = data(permutation(:,1:TRAIN_SIZE),:);
test_set = data(permutation(:,TRAIN_SIZE+1:end),:);

% bextract data (training)
train_attr = train_set(:,bextract); 
train_pos = train_set(:,2);
train_int = train_set(:,3);
train_conf = train_set(:,4);

% bextract data (testing)
test_attr = test_set(:,bextract);
test_pos = test_set(:,2);
test_int = test_set(:,3);
test_conf = test_set(:,4);

%% Create classification trees

tree_pos = ClassificationTree.fit(train_attr, train_pos);
tree_int = ClassificationTree.fit(train_attr, train_int);
tree_conf = ClassificationTree.fit(train_attr, train_conf);

% Now find test results
results_pos = tree_pos.predict(test_attr);
confusion_pos = confusionmat(test_pos, results_pos);

results_int = tree_int.predict(test_attr);
confusion_int = confusionmat(test_int, results_int);

results_conf = tree_conf.predict(test_attr);
confusion_conf = confusionmat(test_conf, results_conf);

accuracy_pos = trace(confusion_pos)/sum(sum(confusion_pos));
accuracy_int = trace(confusion_int)/sum(sum(confusion_int));
accuracy_conf = trace(confusion_conf)/sum(sum(confusion_conf));


















