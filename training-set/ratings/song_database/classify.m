% Garrett Scholtes (modified by Richard Rossi)
% 2017-01-29
%
% Try to perform very basic binary classification on 3rd party database. 

clear all;

data = xlsread('annotations_with_features.xlsx');

% Which subset of bextract values?
% 4:34 gives int 70% and pos 60%
% 4:12 gives int 74% and pos 60%
% 4:15 gives int 67% and pos 60%
% 4:16 gives int 67% and pos 53%
% 4:20 gives int 73% and pos 53%
bextract = 4:12; 

% Make positivity, intensity, confidence binary based on median.
data(:,2) = data(:,2) > median(data(:,2));
data(:,3) = data(:,3) > median(data(:,3));

% Seed
rng(2017);

permutation = randperm(size(data, 1));
% Leave 25 for test
TRAIN_SIZE = 619;
train_set = data(permutation(:,1:TRAIN_SIZE),:);
test_set = data(permutation(:,TRAIN_SIZE+1:end),:);

% bextract data (training)
train_attr = train_set(:,bextract); 
train_pos = train_set(:,2);
train_int = train_set(:,3);

% bextract data (testing)
test_attr = test_set(:,bextract);
test_pos = test_set(:,2);
test_int = test_set(:,3);

%% Create classification trees

tree_pos = ClassificationTree.fit(train_attr, train_pos);
tree_int = ClassificationTree.fit(train_attr, train_int);

% Now find test results
results_pos = tree_pos.predict(test_attr);
confusion_pos = confusionmat(test_pos, results_pos);

results_int = tree_int.predict(test_attr);
confusion_int = confusionmat(test_int, results_int);

accuracy_pos = trace(confusion_pos)/sum(sum(confusion_pos));
accuracy_int = trace(confusion_int)/sum(sum(confusion_int));