% Garrett Scholtes
% 2017-02-03

data = xlsread('..\ratings\song_database\annotations_with_features.xlsx');

% Which subset of attributes to use? 
bextract = 4:12;

% Seed 
rng(2017);

permutation = randperm(size(data, 1));
% Train on 2/3, leave 1/3 for testing 
TRAIN_SIZE = floor(2/3 * size(data, 1));
train_set = data(permutation(:,1:TRAIN_SIZE),:);
test_set = data(permutation(:,TRAIN_SIZE+1:end),:);

% bextract training
train_attr = train_set(:,bextract);
train_pos = train_set(:,2);
train_int = train_set(:,3);

% bextract testing
test_attr = test_set(:,bextract);
test_pos = test_set(:,2);
test_int = test_set(:,3);
std_pos = test_set(:,67);
std_int = test_set(:,66);

%%% Un-comment one of the sections below
%% Linear regression (arbitrary plane - not necessarily through origin)
train_attr_mod = [ones(TRAIN_SIZE,1) train_attr];
test_attr_mod = [ones(size(test_attr,1),1) test_attr];

%% Quadratic regression
%train_attr_mod = [ones(TRAIN_SIZE,1) train_attr train_attr.^2];
%test_attr_mod = [ones(size(test_attr,1),1) test_attr test_attr.^2];


%% Perform regression

regress_pos = train_attr_mod\train_pos;
regress_int = train_attr_mod\train_int;

results_pos = test_attr_mod * regress_pos;
results_int = test_attr_mod * regress_int;

accuracy_pos = sum(abs(results_pos - test_pos) < std_pos)/size(test_set,1);
accuracy_int = sum(abs(results_int - test_int) < std_int)/size(test_set,1);
accuracy = accuracy_pos*accuracy_int;

%% Using NonLinearModel.fit